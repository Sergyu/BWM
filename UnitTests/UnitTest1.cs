using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Tree;
using Core;
using Core.Nodes;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var man = buildContext();
            var model = buildModel(man);
            model.Run(man);

            Console.WriteLine($"man.Age={man.Age}, man.Health={man.Health}");
        }

        [TestMethod]
        public void GetFuncParamTest()
        {
            var line = @"public Func(Object p1, Object p2) ";
            string extractFuncRegex = @"\b[^()]+\((.*)\)$";

            var match = Regex.Match(line, extractFuncRegex);
            string innerArgs = match.Groups[1].Value;
            innerArgs = match.Groups[0].Value;
        }

        [TestMethod]
        public void GetFuncParamTest2()
        {
            string extractFuncRegex = @"\b[^()]+\((.*)\)$";
            

            //Your test string
            string test = @"public Func(Object p1, Object p2) ";

            var match = Regex.Match(test.Trim(), extractFuncRegex);
            string innerArgs = match.Groups[1].Value;
            Assert.AreEqual(innerArgs, @"2 * 7, func2(3, 5)");
        }

        private Man buildContext()
        {
            var man = new Man();
            man.Age = 2;
            man.Health = 10;
            man.Name = "Oshua";
          
            return man;
        }

        private TreeModel buildModel(Man man)
        {
            var growNode = new ActionNode<Man>("GrowAgeAndHealth");
            var goToWork = new DecisionNode<Man>("DecideIFCanWork");
            var startDying = new DecisionNode<Man>("DecideIFDye");
            var workHard = new ActionNode<Man>("Work");
            var deadNode = new ActionNode<Man>("Dead");
            var isSportish = new DecisionNode<Man>("IfSportishContext");
            var fromManToSportish = new TransformationNode<Man, Sport>("FromManToSport");
            var practiceSport = new ActionNode<Sport>("DoSport");
            var sportToMan = new TransformationNode<Sport, Man>("SportToMan");

            growNode.AddChild(goToWork);
            growNode.SetAction(m => { m.Age++; m.Health++; });

            goToWork.SetCondition(m => m.Age > 18);
            goToWork.SetYesChild(workHard);
            goToWork.SetNoChild(growNode);

            workHard.AddChild(isSportish);
            workHard.SetAction(m => { m.Health--; m.Age++; });

            isSportish.SetNoChild(startDying);
            isSportish.SetYesChild(fromManToSportish);
            isSportish.SetCondition(m => m.Age < 60);

            fromManToSportish.AddChild(practiceSport);
            fromManToSportish.SetTranslator(m => new Sport(m.Age));


            practiceSport.SetAction(s => s.Do());
            practiceSport.AddChild(sportToMan);

            sportToMan.SetTranslator(sport =>
            {
                man.Health += sport.GainedHealth;
                return man;
            });
            sportToMan.AddChild(startDying);

            startDying.SetCondition(m => m.Health < 5);
            startDying.SetNoChild(workHard);
            startDying.SetYesChild(deadNode);

            var model = new TreeModel(100);
            model.SetStartNode(growNode);
            model.SetEndNode(deadNode);
            Console.WriteLine("Done");
            return model;
        }
    }


    public class Man
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public int Health { get; internal set; }
    }

    public class Sport
    {
        private int _initAge;
        private int _gainedHealth;

        public Sport(int initialAge)
        {
            this._initAge = initialAge;
        }

        public void Do()
        {
            if (this._initAge >= 10 && this._initAge <= 30)
            {
                this._gainedHealth += 3;
            } else if (this._initAge > 20 && this._initAge <= 80)
            {
                this._gainedHealth += 1;
            }
        }

        public int GainedHealth { get { return this._gainedHealth; } }
    }
}
