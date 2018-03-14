using DesktopClient.UIControls;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace DesktopClient.SigningModel.DomainModel
{
    public class Part
    {
        public bool Signed { get; set; }
        public Signer Signer;
    }

    public enum Available { Always = 1, Sometimes = 3, Rarely = 7 }

    public class Signer
    {
        private int _attempts;
        private Available _type;

        public Signer(Available type)
        {
            this._type = type;
        }

        public void Signe(Part part)
        {
            this._attempts++;
            if (this._attempts >= (int)this._type)
            {
                part.Signed = true;
            }
        }

        public Available Type
        {
            get { return this._type; }
        }
    }

    public class Document
    {
        public List<Part> Parts = new List<Part>();
        public bool IsSigned
        {
            get
            {
                return this.Parts.All(p => p.Signed);
            }
        }
    }
}
