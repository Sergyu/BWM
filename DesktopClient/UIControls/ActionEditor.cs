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
    public partial class ActionEditor : UserControl
    {
        private readonly string domainModelNameSpace = "SigningModel.DomainModel";
        private List<ClassDef> classes;
        private readonly string formater;


        public ActionEditor(bool isConditional = false)
        {
            this.formater = isConditional ? "bool Condition({0} inObject)" : "void ActionFunction({0} inObject)";
            InitializeComponent();
            this.classes = new List<ClassDef>();
            BindData();
        }

        public void SetInType<T>()
        {
            var clasDef = classes.Find(c => c.Name.Equals(typeof(T).Name));
            this.domainObjects.SelectedIndex = classes.IndexOf(clasDef);
            this.groupBoxAction.Text = string.Format(this.formater, this.domainObjects.Text);
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


            lblOut.Visible = true;
            
            this.comboOut.Visible = true;
            this.comboOut.DisplayMember = "Name";
            this.comboOut.ValueMember = "FullName";
            this.comboOut.DataSource = classes;
            this.comboOut.Enabled = false;

        }

        private void DomainObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBoxAction.Text = string.Format(this.formater, this.domainObjects.Text);
        }

        public class ClassDef
        {
            public string Name { get; set; }
            public string FullName { get; set; }
        }
    }
}
