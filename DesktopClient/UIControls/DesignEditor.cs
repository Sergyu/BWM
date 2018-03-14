using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using FastColoredTextBoxNS;
using Common;

namespace DesktopClient.UIControls
{
    public interface ITabClose
    {
        void Close(object sender, EventArgs evt);
    }

    public partial class DesignEditor : UserControl
    {
        private readonly string domainModelNameSpace = "DesktopClient.SigningModel.DomainModel";
        private List<ClassDef> classes;
        private List<ClassDef> classesOut;
        private string formater;
        private FastColoredTextBox tab;
        private EditorType type;

        public enum EditorType { Action, Decision, Start, Split, Transform, Aggregate }

        public DesignEditor(EditorType type = EditorType.Action)
        {
            InitializeComponent();
            this.type = type;
            initializeEditorByType();
            this.classes = new List<ClassDef>();
            var csharpEditor = new CSharpEditor();
            this.tab = csharpEditor.CreateTab(null);
            this.groupBoxAction.Controls.Add(this.tab);
            BindData();
            DomainObjects_SelectedIndexChanged(this, EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = true;
        }

        private void initializeEditorByType()
        {
           // this.lblOut.Visible = this.comboOut.Visible = true;
            string title = "";
            switch (this.type)
            {
                case EditorType.Action:
                    this.formater = "public void ActionFunction({0} inObject)";
                    title = "Define Node Action";
                    break;
                case EditorType.Decision:
                    this.formater = "public bool Condition({0} inObject)";
                    title = "Define Node condition";
                    break;
                case EditorType.Start:
                    this.formater = @"public {0} Initialize()";
                    title = @"Define payload initialization";
                    this.label1.Text = "Init";
                    break;
                case EditorType.Split:
                    this.formater = "public {1} Split({0} inObject, IIdentity childNode)";
                    title = @"Define Splitter Node function";
                    break;
                case EditorType.Transform:
                    this.formater = "public {1} Transform({0} inObject)";
                    title = @"Define Transformation Node function";
                    break;
                case EditorType.Aggregate:
                    this.formater = "public void Aggregate({1} aggregator, {0} childObject)";
                    title = @"Define Aggregation procedure";
                    break;
            }
            this.groupBoxAction.Text = title;
        }

        public void SetInType<T>()
        {
            var clasDef = classes.Find(c => c.Name.Equals(typeof(T).Name));
            this.domainObjects.SelectedIndex = classes.IndexOf(clasDef);
        }

        public void SetOutType<T>()
        {
            var clasDef = classesOut.Find(c => c.Name.Equals(typeof(T).Name));
            this.comboOut.SelectedIndex = classesOut.IndexOf(clasDef);
        }

        private void BindData()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> namespacelist = new List<string>();
            List<string> classlist = new List<string>();

            classes = asm.GetTypes()
                             .Where(t => null != t.Namespace && t.Namespace.Equals(domainModelNameSpace))
                             .Select(t => new ClassDef { Name = t.Name, FullName = t.FullName })
                             .ToList();

            this.domainObjects.DropDownStyle = ComboBoxStyle.DropDownList;
           
            this.domainObjects.DisplayMember = "Name";
            this.domainObjects.ValueMember = "FullName";
            this.domainObjects.DataSource = classes;
            this.domainObjects.SelectedIndexChanged += DomainObjects_SelectedIndexChanged;

            if (this.type == EditorType.Split || this.type == EditorType.Aggregate || this.type == EditorType.Transform)
            {
                classesOut = classes.Select(c => new ClassDef { Name = c.Name, FullName = c.FullName }).ToList();
                this.comboOut.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboOut.DisplayMember = "Name";
                this.comboOut.ValueMember = "FullName";
                this.comboOut.DataSource = classesOut;
                this.comboOut.SelectedIndexChanged += DomainObjects_SelectedIndexChanged;

                this.lblOut.Visible = this.comboOut.Visible = true;
            }
        }

        private string _defaultAction;

        public void SetDefaultAction(string defaultAction)
        {
            this._defaultAction = defaultAction;
        }

        private void DomainObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = string.Format(this.formater, this.domainObjects.Text, this.comboOut.Text);
            str += this._defaultAction.IsNullOrEmpty() ? emptyFunction : this._defaultAction ;
            this.tab.Text = str;
        }

        private const string emptyFunction = @"
{
}";

        public event EventHandler Close;

        private void PctBoxClose_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
            if (null != Close)
            {
                this.Close(sender, e);
            }
        }

        public string GetFunctionBody()
        {
            return this.tab.Text;
        }

        public class ClassDef
        {
            public string Name { get; set; }
            public string FullName { get; set; }
        }
    }
}
