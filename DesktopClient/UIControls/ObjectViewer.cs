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
    public partial class ObjectViewer : UserControl
    {
        public ObjectViewer()
        {
            InitializeComponent();
        }

        public void BindTo(IList<object> payloads, string name)
        {
            this.pnlObjects.SuspendLayout();
            this.lblTitle.Text = "Debugging: " + name + " payloads";
            this.pnlObjects.Controls.Clear();
            int idx = 0;
            var tree = NewTree(idx++, string.Format("Payloads({0})", payloads.Count));
            foreach (var payload in payloads)
            {
                tree.Nodes.Add(buildNode(payload));
                this.pnlObjects.Controls.Add(tree);
              //  tree.ExpandAll();
            }
            this.pnlObjects.ResumeLayout();
        }

        private TreeView NewTree(int idx, string name)
        {
            var tree = new TreeView();
            tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tree.Dock = System.Windows.Forms.DockStyle.Fill;
            tree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tree.ForeColor = System.Drawing.SystemColors.MenuText;
            tree.Location = new System.Drawing.Point(3 + idx*180, 3);
            tree.Name = name+idx;
            tree.ShowLines = true;
            //tree.Size = new System.Drawing.Size(180, 150);
            tree.TabIndex = idx;
            tree.Nodes.Add(new TreeNode(name));
            return tree;
        }

        private TreeNode buildNode(object payload, string name = null)
        {
            var node = new TreeNode();
            var type = payload.GetType();
            if (!IsSimpleType(type))
            {
                node.Text = null == name ? type.Name : name;
                var propFields = type.GetProperties().SelectMany(p => TypeFieldInfo.GetObjects(p, payload));
                var fields = type.GetFields().Select(p => new TypeFieldInfo(p, payload));
                foreach (var p in propFields.Concat(fields).Where(p => p != null))
                {
                    node.Nodes.Add(buildNode(p.Value, p.Name));
                }
            }
            else
            {
                node.Text = string.Format("{0}:{1}", name, payload.ToString());
            }
            return node;
        }

        public static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                type.BaseType.Equals(typeof(Enum));
        }

        class TypeFieldInfo
        {
            
            private object payLoad;

            public TypeFieldInfo(PropertyInfo prop, object payload)
            {
                this.Name = prop.Name;
                this.Value = prop.GetValue(payload);
            }

            public TypeFieldInfo(PropertyInfo prop, object payload, int idx)
            {
                this.Value = prop.GetValue(payload, new object[] { idx});
                this.Name = string.Format("{0}[{1}]", this.Value.GetType().Name, idx);
            }

            public TypeFieldInfo(FieldInfo prop, object payload)
            {
                this.Name = prop.Name;
                this.Value = prop.GetValue(payload);
            }


            public string Name { get; private set; }
            public object Value { get; private set; }

            public static IEnumerable<TypeFieldInfo> GetObjects(PropertyInfo prop, object payload)
            {
                var index = prop.GetIndexParameters().LongLength;
                if (index == 0)
                {
                    if (payload is IEnumerable<object>)
                    {
                        yield return null;
                    }
                    else
                    {
                        yield return new TypeFieldInfo(prop, payload);
                    }
                }
                else
                {
                    int idx = 0;
                    foreach (object o in (payload as IEnumerable<object>))
                    {
                        yield return new TypeFieldInfo(prop, payload, idx++);
                    }
                }
            }
        }
    }
}
