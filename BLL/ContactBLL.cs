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
    public class ContactBLL
    {
        private ContactDAL dal = new ContactDAL();

        /// <summary>
        /// 获取SRM合同号
        /// </summary>
        /// <returns></returns>
        public string GetSRMNo()
        {
            return dal.GetSRMNo();
        }

        /// <summary>
        /// 合同清单列表【根据单号，模糊查询】
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable GetFuzzyQuery(f_SRM_Result model, int pageSize, int pageIndex, ref int total)
        {
            return dal.GetFuzzyQuery(model, pageSize, pageIndex, ref total);
        }

        /// <summary>
        /// 合同清单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public DataTable GetContactList(f_SRM_Result model, int pageSize, int pageIndex, ref int total)
        {
            return dal.GetContactList(model, pageSize, pageIndex, ref total);
        }

        /// <summary>
        /// 根据ID查询合同清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetContactDataByID(string id)
        {
            return dal.GetContactDataByID(id);
        }

        /// <summary>
        /// 查询询价产品
        /// </summary>
        /// <param name="XJNO">询价ID</param>
        /// <returns></returns>
        public DataTable GetContactProduct(string XJNO)
        {
            return dal.GetContactProduct(XJNO);
        }

        /// <summary>
        /// 更新合同状态
        /// </summary>
        /// <param name="id">合同ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int UpdateContactWorkFlowStatus(string id,string status)
        {
            f_SRM_Result model = new f_SRM_Result();
            model.id = id;
            model.WorkFlowStatus = status;

            return dal.UpdateContactWorkFlowStatus(model);
        }

        /// <summary>
        /// 更新合同状态
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int UpdateContactWorkFlowStatus(IList<f_SRM_Result> modelList)
        {
            return dal.UpdateContactWorkFlowStatus(modelList);
        }

        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="model">分店</param>
        /// <returns></returns>
        public int UpdateContactInfo(f_SRM_Result model)
        {
            return dal.UpdateContactInfo(model);
        }

        /// <summary>
        /// 保存合同产品
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int AddContactProduct(IList<ContactProduct> modelList)
        {
            return dal.AddContactProduct(modelList);
        }

         /// <summary>
        /// 根据合同表ID查询产品
        /// </summary>
        /// <param name="cid">合同表ID</param>
        /// <returns></returns>
        public DataTable GetContactProductByCID(string cid)
        {
            return dal.GetContactProductByCID(cid);
        }
    }
}
