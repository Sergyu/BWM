using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DesktopClient.UIControls
{
    public partial class SplitterEditor : UserControl
    {
        private readonly string domainModelNameSpace = "SigningModel.DomainModel";
        private List<ClassDef> classesIn;
        private List<ClassDef> classesOut;
        public enum TransformType { Split = 0, Transform = 1, Aggregate = 2}

        private string formatter;

        public SplitterEditor(TransformType type = TransformType.Split)
        {
            setFormatter(type);
            InitializeComponent();
            this.classesIn = new List<ClassDef>();
            this.classesOut = new List<ClassDef>();
            BindData();
        }

        private void setFormatter(TransformType type)
        {
            switch (type)
            {
                case TransformType.Split:
                    this.formatter = "{0} Split({1} inObject, IIdentifier childNode)";
                    break;
                case TransformType.Transform:
                    this.formatter = "{0} Transform({1} inObject)";
                    break;
                case TransformType.Aggregate:
                    this.formatter = "void Aggregate({0} aggregator, {1} childObject)";
                    break;
            }
        }

        public void SetInType<T>()
        {
            var clasDef = classesIn.Find(c => c.Name.Equals(typeof(T).Name));
            this.domainObjects1.SelectedIndex = classesIn.IndexOf(clasDef);
        }

        public void SetOutType<T>()
        {
           var clasDef = classesOut.Find(c => c.Name.Equals(typeof(T).Name));
           this.comboOut1.SelectedIndex = classesOut.IndexOf(clasDef);
        }

        private void BindData()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> namespacelist = new List<string>();
            List<string> classlist = new List<string>();

            classesIn = asm.GetTypes()
                             .Where(t => null != t.Namespace && t.Namespace.Equals(domainModelNameSpace))
                             .Select(t => new ClassDef { Name = t.Name, FullName = t.FullName })
                             .ToList();

            this.domainObjects1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            this.domainObjects1.DisplayMember = "Name";
            this.domainObjects1.ValueMember = "FullName";
            this.domainObjects1.DataSource = classesIn;
            this.domainObjects1.SelectedIndexChanged += DomainObjects_SelectedIndexChanged;

            classesOut = classesIn.Select(c => new ClassDef { Name = c.Name, FullName = c.FullName }).ToList();

            
            this.comboOut1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            
            this.comboOut1.DisplayMember = "Name";
            this.comboOut1.ValueMember = "FullName";
            this.comboOut1.DataSource = classesOut;
            this.comboOut1.SelectedIndexChanged += DomainObjects_SelectedIndexChanged;

        }

        private void DomainObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBoxAction.Text = string.Format(this.formatter, this.comboOut1.Text, this.domainObjects1.Text);
        }

        

        public class ClassDef
        {
            public string Name { get; set; }
            public string FullName { get; set; }
        }
    }
}
