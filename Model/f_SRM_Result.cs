using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class f_SRM_Result
    {
        /// <summary>
        /// id
        /// </summary>
        public virtual string id
        {
            get;
            set;
        }
        /// <summary>
        /// XunJiaWorkflowID
        /// </summary>
        public virtual string XunJiaWorkflowID
        {
            get;
            set;
        }
        /// <summary>
        /// DistributorCode
        /// </summary>
        public virtual string DistributorCode
        {
            get;
            set;
        }
        /// <summary>
        /// OAAuditStatus
        /// </summary>
        public virtual int? OAAuditStatus
        {
            get;
            set;
        }
        /// <summary>
        /// OAAuditDate
        /// </summary>
        public virtual DateTime? OAAuditDate
        {
            get;
            set;
        }
        /// <summary>
        /// SRM_Contract_NO
        /// </summary>
        public virtual string SRM_Contract_NO
        {
            get;
            set;
        }
        /// <summary>
        /// ERP_Contract_NO
        /// </summary>
        public virtual string ERP_Contract_NO
        {
            get;
            set;
        }
        /// <summary>
        /// GrantNO
        /// </summary>
        public virtual string GrantNO
        {
            get;
            set;
        }
        /// <summary>
        /// TemplateId
        /// </summary>
        public virtual string TemplateId
        {
            get;
            set;
        }
        /// <summary>
        /// WorkFlowStatus
        /// </summary>
        public virtual string WorkFlowStatus
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// BuyerAccount
        /// </summary>
        public virtual string BuyerAccount
        {
            get;
            set;
        }
        /// <summary>
        /// CSRMSerialNO
        /// </summary>
        public virtual string CSRMSerialNO
        {
            get;
            set;
        }
        /// <summary>
        /// PriceClause
        /// </summary>
        public virtual string PriceClause
        {
            get;
            set;
        }
        /// <summary>
        /// AgreementBDate
        /// </summary>
        public virtual string AgreementBDate
        {
            get;
            set;
        }
        /// <summary>
        /// AgreementEDate
        /// </summary>
        public virtual string AgreementEDate
        {
            get;
            set;
        }
        /// <summary>
        /// Remark
        /// </summary>
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// DistributorRemark
        /// </summary>
        public virtual string DistributorRemark
        {
            get;
            set;
        }

        /// <summary>
        /// 合同金额
        /// </summary>
        public virtual decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 采购经理审批回退原因
        /// </summary>
        public virtual string AuditRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public virtual string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual string PlayType
        {
            get;
            set;
        }
    }
}
