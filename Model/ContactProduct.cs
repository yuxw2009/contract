using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContactProduct
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// CID
        /// </summary>
        public virtual string CID
        {
            get;
            set;
        }
        /// <summary>
        /// CType
        /// </summary>
        public virtual string CType
        {
            get;
            set;
        }
        /// <summary>
        /// ItemCode
        /// </summary>
        public virtual string ItemCode
        {
            get;
            set;
        }
        /// <summary>
        /// CVersion
        /// </summary>
        public virtual string CVersion
        {
            get;
            set;
        }
        /// <summary>
        /// Classificatiion
        /// </summary>
        public virtual string Classificatiion
        {
            get;
            set;
        }
        /// <summary>
        /// ItemName
        /// </summary>
        public virtual string ItemName
        {
            get;
            set;
        }
        /// <summary>
        /// Unit
        /// </summary>
        public virtual string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// Quantity
        /// </summary>
        public virtual string Quantity
        {
            get;
            set;
        }
        /// <summary>
        /// UnitPrice
        /// </summary>
        public virtual string UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// PromiseDate
        /// </summary>
        public virtual DateTime? PromiseDate
        {
            get;
            set;
        }
        /// <summary>
        /// RequireDate
        /// </summary>
        public virtual DateTime? RequireDate
        {
            get;
            set;
        }
        /// <summary>
        /// CRemark
        /// </summary>
        public virtual string CRemark
        {
            get;
            set;
        } 
    }
}
