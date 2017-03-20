using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 合同模板BLL
    /// </summary>
    public class ContactTempletBLL
    {
        private readonly ContactTempletDAL dal = new ContactTempletDAL();

        /// <summary>
        /// 获取采购组织信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPRProject()
        {
            return dal.GetPRProject();
        }

        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="ctCode">编号</param>
        /// <param name="id">id号，如果新建则为空</param>
        /// <returns></returns>
        public bool ExistsCTCode(string ctCode, string id)
        {
            //编号为空，则直接返回存在
            if (string.IsNullOrEmpty(ctCode))
            {
                return true;
            }

            return dal.ExistsCTCode(ctCode, id);
        }

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="ctName">名称</param>
        /// <param name="id">id号，如果新建则为空</param>
        /// <returns></returns>
        public bool ExistsCTName(string ctName, string id)
        {
            //名称为空，则直接返回存在
            if (string.IsNullOrEmpty(ctName))
            {
                return true;
            }

            return dal.ExistsCTName(ctName, id);
        }

        /// <summary>
        /// 获取模板编号
        /// </summary>
        /// <returns></returns>
        public string GetCTCode(string projectNr)
        {
            if (string.IsNullOrEmpty(projectNr))
            {
                return "";
            }
            return dal.GetCTCode(projectNr);
        }

        /// <summary>
        /// 获取模板版本号
        /// </summary>
        /// <returns></returns>
        public string GetCTVersion(string ctName)
        {
            if (string.IsNullOrEmpty(ctName))
            {
                return "";
            }
            return dal.GetCTVersion(ctName);

        }

        /// <summary>
        /// 保存合同模板
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int AddContactTemplet(ContactTemplet model)
        {
            return dal.AddContactTemplet(model);
        }

        /// <summary>
        /// 复制模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns></returns>
        public int CopyContractTempleet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }

            return dal.CopyContractTempleet(id);
        }

        /// <summary>
        /// 查询合同模板信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetGetContactTemplet(string id)
        {
            ContactTemplet model = new ContactTemplet();
            model.ID = id;

            int total = 0;
            return dal.GetContactTemplet(model, 1, 1, ref total);
        }

        /// <summary>
        /// 查询合同模板【适用下拉选项】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetContactTempletOption(ContactTemplet model)
        {
            return dal.GetContactTempletOption(model);
        }

        /// <summary>
        /// 查询合同模板(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetContactTemplet(ContactTemplet model, int pageSize, int pageIndex, ref int total)
        {
            return dal.GetContactTemplet(model, pageSize, pageIndex, ref total);
        }

        /// <summary>
        /// 查询合同模板【模糊查询】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetFuzzyQuery(ContactTemplet model, int pageSize, int pageIndex, ref int total)
        {
            return dal.GetFuzzyQuery(model, pageSize, pageIndex, ref total);
        }

        /// <summary>
        /// 更新合同模板信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateContactTemplet(ContactTemplet model)
        {
            return dal.UpdateContactTemplet(model);
        }

        /// <summary>
        /// 更新合同模板信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int UpdateContactTemplet(IList<ContactTemplet> modelList)
        {
            return dal.UpdateContactTemplet(modelList);
        }
    }
}
