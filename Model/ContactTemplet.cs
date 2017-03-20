using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 合同模板
    /// </summary>
    public class ContactTemplet
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual string ID
        {
            get;
            set;
        }
        /// <summary>
        /// CTCode
        /// </summary>
        public virtual string CTCode
        {
            get;
            set;
        }
        /// <summary>
        /// CTVersion
        /// </summary>
        public virtual string CTVersion
        {
            get;
            set;
        }
        /// <summary>
        /// CTName
        /// </summary>
        public virtual string CTName
        {
            get;
            set;
        }
        /// <summary>
        /// CTBusiness
        /// </summary>
        public virtual string CTBusiness
        {
            get;
            set;
        }
        /// <summary>
        /// CTPurchaseOrg
        /// </summary>
        public virtual string CTPurchaseOrg
        {
            get;
            set;
        }
        /// <summary>
        /// CTBuyer
        /// </summary>
        public virtual string CTBuyer
        {
            get;
            set;
        }
        /// <summary>
        /// CTSupplier
        /// </summary>
        public virtual string CTSupplier
        {
            get;
            set;
        }
        /// <summary>
        /// CTRemark
        /// </summary>
        public virtual string CTRemark
        {
            get;
            set;
        }
        /// <summary>
        /// CTCreate
        /// </summary>
        public virtual string CTCreate
        {
            get;
            set;
        }
        /// <summary>
        /// CTCreateTime
        /// </summary>
        public virtual DateTime? CTCreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// CTStatus
        /// </summary>
        public virtual string CTStatus
        {
            get;
            set;
        }
        /// <summary>
        /// IsDefault
        /// </summary>
        public virtual string IsDefault
        {
            get;
            set;
        }
        /// <summary>
        /// ProjectNr
        /// </summary>
        public virtual string ProjectNr
        {
            get;
            set;
        }
        /// <summary>
        /// 替换参数
        /// </summary>
        public virtual string Parameter
        {
            get;
            set;
        }
    }
}
