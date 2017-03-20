using FuYao.DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class ContactDAL
    {

        /// <summary>
        /// 获取SRM合同号
        /// </summary>
        /// <returns></returns>
        public string GetSRMNo()
        {
            string srmNo = "";
            StringBuilder strSql = new StringBuilder();//SQL语句
            strSql.Append("select (case when max(convert(int,SRM_Contract_NO)) is NULL then 90000001 when max(convert(int,SRM_Contract_NO)) = '' THEN 90000001 else max(convert(int,SRM_Contract_NO))+1 end) SRMNO from f_SRM_Result");

            DataTable dt = null;
            //返回多表查询结果
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    srmNo = dt.Rows[0][0].ToString();
                }
            }
            if (string.IsNullOrEmpty(srmNo))
            {
                srmNo = "90000001";
            }
            return srmNo;
        }

        /// <summary>
        /// 动态添加查询参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlParameter[] SetSqlParamenter(ref StringBuilder strSql, f_SRM_Result model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;

                if (!string.IsNullOrEmpty(model.WorkFlowStatus))//作为多个状态查询时使用
                {
                    strSql.Append(" and WorkFlowStatus in( ");
                    string[] arList = model.WorkFlowStatus.Split(new char[] { ',' });
                    for (int i = 0; i < arList.Length; i++)
                    {
                        string Status = arList[i].ToString().Trim();
                        if (i > 0) { strSql.Append(","); }
                        string parId = "@WorkFlowStatus" + i.ToString();
                        strSql.Append(parId);

                        para = new SqlParameter(parId, SqlDbType.NVarChar, 50);
                        para.Value = Status;
                        splist.Add(para);
                    }
                    strSql.Append(") ");
                }

                //if (!string.IsNullOrEmpty(model.WorkFlowStatus))
                //{
                //    strSql.Append(" and WorkFlowStatus in(@WorkFlowStatus) ");
                //    para = new SqlParameter("@WorkFlowStatus", SqlDbType.NVarChar, 50);
                //    para.Value = model.WorkFlowStatus;
                //    splist.Add(para);
                //}
                if (!string.IsNullOrEmpty(model.id))
                {
                    strSql.Append(" and id = @id ");
                    para = new SqlParameter("@id", SqlDbType.UniqueIdentifier, 16);
                    para.Value = new Guid(model.id);
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.SRM_Contract_NO))
                {
                    strSql.Append(" and SRM_Contract_NO = @SRM_Contract_NO ");
                    para = new SqlParameter("@SRM_Contract_NO", SqlDbType.NVarChar, 60);
                    para.Value = model.SRM_Contract_NO;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.XunJiaWorkflowID))
                {
                    strSql.Append(" and XunJiaWorkflowID = @XunJiaWorkflowID ");
                    para = new SqlParameter("@XunJiaWorkflowID", SqlDbType.NVarChar, 60);
                    para.Value = model.XunJiaWorkflowID;
                    splist.Add(para);
                }
            }

            SqlParameter[] param = null;
            if (splist.Count > 0)
            {
                param = splist.ToArray();
            }
            return param;
        }

        /// <summary>
        /// 动态添加模糊查询参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlParameter[] SetSqlParamenter2(ref StringBuilder strSql, f_SRM_Result model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;

                if (!string.IsNullOrEmpty(model.WorkFlowStatus))//作为多个状态查询时使用
                {
                    strSql.Append(" and WorkFlowStatus in( ");
                    string[] arList = model.WorkFlowStatus.Split(new char[] { ',' });
                    for (int i = 0; i < arList.Length; i++)
                    {
                        string Status = arList[i].ToString().Trim();
                        if (i > 0) { strSql.Append(","); }
                        string parId = "@WorkFlowStatus" + i.ToString();
                        strSql.Append(parId);

                        para = new SqlParameter(parId, SqlDbType.NVarChar, 50);
                        para.Value = Status;
                        splist.Add(para);
                    }
                    strSql.Append(") ");
                }
                //if (!string.IsNullOrEmpty(model.WorkFlowStatus))
                //{
                //    strSql.Append(" and WorkFlowStatus in(@WorkFlowStatus) ");
                //    para = new SqlParameter("@WorkFlowStatus", SqlDbType.NVarChar, 50);
                //    para.Value = model.WorkFlowStatus;
                //    splist.Add(para);
                //}
                if (!string.IsNullOrEmpty(model.SRM_Contract_NO))
                {
                    strSql.Append(" and (SRM_Contract_NO like @SRM_Contract_NO ");
                    para = new SqlParameter("@SRM_Contract_NO", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);
                //}
                //if (!string.IsNullOrEmpty(model.XunJiaWorkflowID))
                //{
                    strSql.Append(" or srm.XunJiaWorkflowID like @XunJiaWorkflowID ");
                    para = new SqlParameter("@XunJiaWorkflowID", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);
                //}
                //if (!string.IsNullOrEmpty(model.ERP_Contract_NO))
                //{
                    strSql.Append(" or ERP_Contract_NO like @ERP_Contract_NO ");
                    para = new SqlParameter("@ERP_Contract_NO", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);

                    strSql.Append(" or srm.DistributorCode like @DistributorCode ");
                    para = new SqlParameter("@DistributorCode", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);

                    strSql.Append(" or di.DistributorName like @DistributorName ");
                    para = new SqlParameter("@DistributorName", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);

                    strSql.Append(" or di.ContactName like @ContactName ");
                    para = new SqlParameter("@ContactName", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);

                    strSql.Append(" or ppj.Description like @Description) ");
                    para = new SqlParameter("@Description", SqlDbType.NVarChar, 60);
                    para.Value = "%" + model.SRM_Contract_NO + "%";
                    splist.Add(para);
                }
            }

            SqlParameter[] param = null;
            if (splist.Count > 0)
            {
                param = splist.ToArray();
            }
            return param;
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
            try
            {
                StringBuilder strSql = new StringBuilder();//SQL语句
                StringBuilder whereSql = new StringBuilder();//SQL条件

                SqlParameter[] param = null;
                //动态添加参数
                if (model != null)
                {
                    param = SetSqlParamenter2(ref whereSql, model);
                }

                strSql.Append("select top " + pageSize + " A.* ");
                strSql.Append(" from(");
                strSql.Append("select top " + (pageSize * pageIndex) + " ROW_NUMBER() over(order by CreateTime desc) as rowno,");
                strSql.Append(" srm.* ");
                strSql.Append(" ,di.DistributorName,di.DistributorAddr,di.ContactName,");
                strSql.Append(" ab.Currency as AbCurrency");
                strSql.Append(" ,ppj.Description as BuyOrg,ppj.Description as ReceivingParty,ppj.Description as AcquiringParty");
                strSql.Append(",(select CTName from ContactTemplet where ID=srm.TemplateId) as TemplateName ");

                strSql.Append(" from f_SRM_Result srm ");
                strSql.Append(" left join Absences ab on srm.XunJiaWorkflowID = ab.Description and LTRIM(srm.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join DistributorItems di on di.XunJiaWorkflowID = ab.Description and LTRIM(di.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join PRProject ppj on ab.ProjectNumber = ppj.ProjectNr ");

                strSql.Append(" where 1=1 ");
                strSql.Append(whereSql.ToString());

                //查询产品子单，返回主单ID
                StringBuilder strP = new StringBuilder();
                StringBuilder strSql2 = new StringBuilder();
                StringBuilder strSql3 = new StringBuilder();
                StringBuilder strSql4 = new StringBuilder();
                string queryStr = model.SRM_Contract_NO;

                strSql2.Append(" and WorkFlowStatus in("+model.WorkFlowStatus+") and (pr.ItemCode='" + queryStr + "' or pr.ItemName='" + queryStr + "' or pr.XunJiaWorkflowID='" + queryStr + "') ");
                strSql3.Append(" and WorkFlowStatus in(" + model.WorkFlowStatus + ") and (bpr.ItemCode='" + queryStr + "' or bpr.ItemName='" + queryStr + "' or bpr.XunJiaWorkflowID='" + queryStr + "') ");
                strSql4.Append(" and WorkFlowStatus in(" + model.WorkFlowStatus + ") and (cp.ItemCode='" + queryStr + "' or cp.ItemName='" + queryStr + "') ");

                strP.Append(" or srm.id in(");
                strP.Append("select srm.id from f_SRM_Result srm ");
                strP.Append(" left join pr pr on pr.XunJiaWorkflowID = srm.XunJiaWorkflowID ");//pr表
                strP.Append(" where 1=1 " + strSql2.ToString());
                strP.Append(" union all ");
                strP.Append("select srm.id from f_SRM_Result srm ");
                strP.Append(" left join BPR bpr on bpr.XunJiaWorkflowID = srm.XunJiaWorkflowID ");//BPR表
                strP.Append(" where 1=1 " + strSql3.ToString());
                strP.Append(" union all ");
                strP.Append("select srm.id from f_SRM_Result srm ");
                strP.Append(" left join ContactProduct cp on cp.cid = srm.id ");//合同产品表（生成合同后保存的表）
                strP.Append(" where 1=1 " + strSql4.ToString());
                strP.Append(" group by srm.id) ");

                strSql.Append(strP.ToString());

                strSql.Append(" )as A ");
                //分页
                strSql.Append(" where A.rowno>" + (pageSize * (pageIndex - 1)) + " ");

                //查询总条数条件
                StringBuilder totalStr = new StringBuilder();
                totalStr.Append("select count(srm.id) FROM f_SRM_Result srm ");
                totalStr.Append(" left join Absences ab on srm.XunJiaWorkflowID = ab.Description and LTRIM(srm.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                totalStr.Append(" left join DistributorItems di on di.XunJiaWorkflowID = ab.Description and LTRIM(di.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                totalStr.Append(" left join PRProject ppj on ab.ProjectNumber = ppj.ProjectNr ");
                totalStr.Append(" where 1=1 ");
                totalStr.Append(whereSql.ToString());

                //查询产品子单，返回主单ID
                //totalStr.Append(" or srm.id in(");
                //totalStr.Append("select srm.id from f_SRM_Result srm ");
                //totalStr.Append(" left join pr pr on pr.XunJiaWorkflowID = srm.XunJiaWorkflowID ");//pr表
                //totalStr.Append(" where 1=1 and (pr.ItemCode='2298' or pr.ItemName='2298' or pr.XunJiaWorkflowID='2298') ");
                //totalStr.Append(" union all ");
                //totalStr.Append("select srm.id from f_SRM_Result srm ");
                //totalStr.Append(" left join BPR bpr on bpr.XunJiaWorkflowID = srm.XunJiaWorkflowID ");//BPR表
                //totalStr.Append(" where 1=1 and (bpr.ItemCode='2298' or bpr.ItemName='2298' or bpr.XunJiaWorkflowID='2298') ");
                //totalStr.Append(" union all ");
                //totalStr.Append("select srm.id from f_SRM_Result srm ");
                //totalStr.Append(" left join ContactProduct cp on cp.cid = srm.id ");//合同产品表（生成合同后保存的表）
                //totalStr.Append(" where 1=1 and (cp.ItemCode='2298' or cp.ItemName='2298') ");
                //totalStr.Append(" group by srm.id) ");

                totalStr.Append(strP.ToString());


                //先查询产品再查询主单
                //strSql.Append("select srm.*  ");
                //strSql.Append(" ,di.DistributorName,di.DistributorAddr,di.ContactName,");
                //strSql.Append(" ab.Currency as AbCurrency");
                //strSql.Append(" ,ppj.Description as BuyOrg,ppj.Description as ReceivingParty,ppj.Description as AcquiringParty");

                //strSql.Append(" from(");

                //strSql.Append("select top " + pageSize + " A.* ");
                //strSql.Append(" from(");
                //strSql.Append("select top " + (pageSize * pageIndex) + " ROW_NUMBER() over(order by CreateTime desc) as rowno,");
                //strSql.Append(" * ");
                //strSql.Append(" from f_SRM_Result ");

                //strSql.Append(" where 1=1 ");
                //strSql.Append(whereSql.ToString());

                //strSql.Append(" )as A ");
                ////分页
                //strSql.Append(" where A.rowno>" + (pageSize * (pageIndex - 1)) + " ");

                //strSql.Append(") as srm ");
                //strSql.Append(" left join Absences ab on srm.XunJiaWorkflowID = ab.Description and LTRIM(srm.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                //strSql.Append(" left join DistributorItems di on di.XunJiaWorkflowID = ab.Description and LTRIM(di.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                //strSql.Append(" left join PRProject ppj on ab.ProjectNumber = ppj.ProjectNr ");

                ////查询总条数条件
                //StringBuilder totalStr = new StringBuilder();
                //totalStr.Append("select count(s.id) ");
                //totalStr.Append(" from f_SRM_Result s ");

                //totalStr.Append(" where 1=1 ");
                //totalStr.Append(whereSql.ToString());

                //total = int.Parse(DbHelperSQL.GetSingle(totalStr.ToString(), param).ToString());//查询总条数

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(totalStr.ToString() + ";" + strSql.ToString(), param);
                if (ds != null && ds.Tables.Count > 1)
                {
                    total = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    dt = ds.Tables[1];
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 合同清单列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable GetContactList(f_SRM_Result model, int pageSize, int pageIndex, ref int total)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();//SQL语句
                StringBuilder whereSql = new StringBuilder();//SQL条件

                SqlParameter[] param = null;
                //动态添加参数
                if (model != null)
                {
                    param = SetSqlParamenter(ref whereSql, model);
                }

                strSql.Append("select srm.*  ");
                strSql.Append(" ,di.DistributorName,di.DistributorAddr,di.ContactName,");
                strSql.Append(" ab.Currency as AbCurrency");
                strSql.Append(" ,ppj.Description as BuyOrg,ppj.Description as ReceivingParty,ppj.Description as AcquiringParty");
                strSql.Append(",(select CTName from ContactTemplet where ID=convert(uniqueidentifier,srm.TemplateId)) as TemplateName ");
                strSql.Append(" from(");

                strSql.Append("select top " + pageSize + " A.* ");
                strSql.Append(" from(");
                strSql.Append("select top " + (pageSize * pageIndex) + " ROW_NUMBER() over(order by CreateTime desc) as rowno,");
                strSql.Append(" * ");
                strSql.Append(" from f_SRM_Result ");

                strSql.Append(" where 1=1 ");
                strSql.Append(whereSql.ToString());

                strSql.Append(" )as A ");
                //分页
                strSql.Append(" where A.rowno>" + (pageSize * (pageIndex - 1)) + " ");

                strSql.Append(") as srm ");
                strSql.Append(" left join Absences ab on srm.XunJiaWorkflowID = ab.Description and LTRIM(srm.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join DistributorItems di on di.XunJiaWorkflowID = ab.Description and LTRIM(di.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join PRProject ppj on ab.ProjectNumber = ppj.ProjectNr ");

                //查询总条数条件
                StringBuilder totalStr = new StringBuilder();
                totalStr.Append("select count(s.id) ");
                totalStr.Append(" from f_SRM_Result s ");

                totalStr.Append(" where 1=1 ");
                totalStr.Append(whereSql.ToString());

                //total = int.Parse(DbHelperSQL.GetSingle(totalStr.ToString(), param).ToString());//查询总条数

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(totalStr.ToString() + ";" + strSql.ToString(), param);
                if (ds != null && ds.Tables.Count > 1)
                {
                    total = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    dt = ds.Tables[1];
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID查询合同清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetContactDataByID(string id)
        {
            if (string.IsNullOrEmpty(id)) { return null; }
            try
            {
                StringBuilder strSql = new StringBuilder();//SQL语句

                strSql.Append("select srm.*  ");
                strSql.Append(" ,di.DistributorName,di.DistributorAddr,di.ContactName,");
                strSql.Append(" ab.Currency as AbCurrency ");
                strSql.Append(" ,ppj.Description as BuyOrg,ppj.Description as ReceivingParty,ppj.Description as AcquiringParty");
                strSql.Append(",(select CTName from ContactTemplet where ID=srm.TemplateId) as TemplateName ");
                strSql.Append(" from(");

                strSql.Append("select * ");
                strSql.Append(" from f_SRM_Result ");
                strSql.Append(" where 1=1 ");
                strSql.Append(" and id='" + id + "'");

                strSql.Append(") as srm ");
                strSql.Append(" left join Absences ab on srm.XunJiaWorkflowID = ab.Description and LTRIM(srm.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join DistributorItems di on di.XunJiaWorkflowID = ab.Description and LTRIM(di.DistributorCode) =LTRIM(ab.FreeTextField_15) ");
                strSql.Append(" left join PRProject ppj on ab.ProjectNumber = ppj.ProjectNr ");

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 更新合同状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateContactWorkFlowStatus(f_SRM_Result model)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("update f_SRM_Result set ");

                //动态添加参数
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.id.ToString())) { return 0; }

                    strSql.Append(" WorkFlowStatus ='" + model.WorkFlowStatus + "' ");
                    strSql.Append(" where id='" + model.id + "' ");
                }

                //执行数据库操作
                int result = DbHelperSQL.ExecuteSql(strSql.ToString());
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新合同状态
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int UpdateContactWorkFlowStatus(IList<f_SRM_Result> modelList)
        {
            try
            {
                Dictionary<string, SqlParameter[]> Dict = new Dictionary<string, SqlParameter[]>();
                int keyIndex = 0;
                StringBuilder strSql = new StringBuilder();
                foreach (f_SRM_Result item in modelList)
                {
                    strSql.Append("update f_SRM_Result set ");
                    strSql.Append("WorkFlowStatus=@WorkFlowStatus");
                    strSql.Append(" where id=@id;");

                    SqlParameter[] parameters = {
                    new SqlParameter("@WorkFlowStatus", SqlDbType.NVarChar, 50),
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier, 16)
                                                };
                    parameters[0].Value = item.WorkFlowStatus;
                    parameters[1].Value = new Guid(item.id);

                    Dict.Add(keyIndex + "&" + strSql.ToString(), parameters);
                    strSql = new StringBuilder();
                    keyIndex++;
                }

                //执行数据库操作
                int result =DbHelperSQL.ExecuteSqlListTrans(Dict);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateContactInfo(f_SRM_Result model)
        {
            Dictionary<string, SqlParameter[]> Dict = new Dictionary<string, SqlParameter[]>();
            int keyIndex = 0;
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("update f_SRM_Result set ");

                SqlParameter[] param = null;
                //动态添加参数
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.id.ToString())) { return 0; }
                    param = SetUpdataSqlParamenter(ref strSql, model);

                    //Dict.Add(keyIndex + "&" + strSql.ToString(), param);
                    //strSql = new StringBuilder();
                    //keyIndex++;
                }

                //执行数据库操作
                int result = DbHelperSQL.ExecuteSql(strSql.ToString(), param);// .ExecuteSqlListTrans(Dict);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 动态添加更新参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlParameter[] SetUpdataSqlParamenter(ref StringBuilder strSql, f_SRM_Result model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;
                if (!string.IsNullOrEmpty(model.WorkFlowStatus.ToString()))
                {
                    strSql.Append(" WorkFlowStatus = @WorkFlowStatus ");
                    para = new SqlParameter("@WorkFlowStatus", SqlDbType.NVarChar, 50);
                    para.Value = model.WorkFlowStatus;
                    splist.Add(para);
                }

                //如果合同号为空，则生成添加
                if (string.IsNullOrEmpty(model.SRM_Contract_NO))
                {
                    strSql.Append(" ,SRM_Contract_NO = (select (case when max(convert(int,SRM_Contract_NO)) is NULL then 90000001 when max(convert(int,SRM_Contract_NO)) = '' THEN 90000001 else max(convert(int,SRM_Contract_NO))+1 end) SRMNO from f_SRM_Result) ");
                }
                if (!string.IsNullOrEmpty(model.PriceClause))
                {
                    strSql.Append(" ,PriceClause = @PriceClause ");
                    para = new SqlParameter("@PriceClause", SqlDbType.NVarChar, 50);
                    para.Value = model.PriceClause;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.TemplateId))
                {
                    strSql.Append(" ,TemplateId = @TemplateId ");
                    para = new SqlParameter("@TemplateId", SqlDbType.NVarChar, 60);
                    para.Value = model.TemplateId;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.AgreementBDate))
                {
                    strSql.Append(" ,AgreementBDate = @AgreementBDate ");
                    para = new SqlParameter("@AgreementBDate", SqlDbType.NVarChar, 20);
                    para.Value = model.AgreementBDate;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.AgreementEDate))
                {
                    strSql.Append(" ,AgreementEDate = @AgreementEDate ");
                    para = new SqlParameter("@AgreementEDate", SqlDbType.NVarChar, 20);
                    para.Value = model.AgreementEDate;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CreateTime.ToString()))
                {
                    strSql.Append(" ,CreateTime = @CreateTime ");
                    para = new SqlParameter("@CreateTime", SqlDbType.DateTime);
                    para.Value = model.CreateTime;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.Remark))
                {
                    strSql.Append(" ,Remark = @Remark ");
                    para = new SqlParameter("@Remark", SqlDbType.NVarChar, 500);
                    para.Value = model.Remark;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.DistributorRemark))
                {
                    strSql.Append(" ,DistributorRemark = @DistributorRemark ");
                    para = new SqlParameter("@DistributorRemark", SqlDbType.NVarChar, 500);
                    para.Value = model.DistributorRemark;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.Amount.ToString()) && model.Amount > 0)
                {
                    strSql.Append(" ,Amount = @Amount ");
                    para = new SqlParameter("@Amount", SqlDbType.Decimal);
                    para.Value = model.Amount;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.AuditRemark))
                {
                    strSql.Append(" ,AuditRemark = @AuditRemark ");
                    para = new SqlParameter("@AuditRemark", SqlDbType.NVarChar, 500);
                    para.Value = model.AuditRemark;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.Currency))
                {
                    strSql.Append(" ,Currency = @Currency ");
                    para = new SqlParameter("@Currency", SqlDbType.NVarChar, 20);
                    para.Value = model.Currency;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.PlayType))
                {
                    strSql.Append(" ,PlayType = @PlayType ");
                    para = new SqlParameter("@PlayType", SqlDbType.NVarChar, 50);
                    para.Value = model.PlayType;
                    splist.Add(para);
                }

                strSql.Append(" where id=@id ");
                para = new SqlParameter("@id", SqlDbType.UniqueIdentifier, 16);
                para.Value = new Guid(model.id);
                splist.Add(para);
            }

            SqlParameter[] param = null;
            if (splist.Count > 0)
            {
                param = splist.ToArray();
            }
            return param;
        }


        #region 合同产品

        /// <summary>
        /// 查询询价产品
        /// </summary>
        /// <param name="XJNO">询价ID</param>
        /// <returns></returns>
        public DataTable GetContactProduct(string XJNO)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@XJNO", SqlDbType.VarChar, 255)
                    };
            parameters[0].Value = XJNO;

            DataSet ds = DbHelperSQL.RunProcedure("f_save_contract", parameters, "ds");

            return ds.Tables[0];
        }

        /// <summary>
        /// 保存合同产品
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int AddContactProduct(IList<ContactProduct> modelList)
        {
            try
            {
                Dictionary<string, SqlParameter[]> Dict = new Dictionary<string, SqlParameter[]>();
                int keyIndex = 0;
                StringBuilder strSql = new StringBuilder();

                if (modelList != null)
                {
                    strSql.Append(" delete from ContactProduct where CID='" + modelList[0].CID + "';");//删除原来的数据
                    Dict.Add(keyIndex + "&" + strSql.ToString(), null);
                    strSql = new StringBuilder();
                    keyIndex++;

                    foreach (ContactProduct item in modelList)
                    {
                        strSql.Append("insert into ContactProduct(");
                        strSql.Append("ID,UnitPrice,PromiseDate,RequireDate,CRemark,CID,CType,ItemCode,CVersion,Classificatiion,ItemName,Unit,Quantity");
                        strSql.Append(") values (");
                        strSql.Append("@ID,@UnitPrice,@PromiseDate,@RequireDate,@CRemark,@CID,@CType,@ItemCode,@CVersion,@Classificatiion,@ItemName,@Unit,@Quantity");
                        strSql.Append(") ");

                        SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@UnitPrice", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@PromiseDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@RequireDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CRemark", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@CID", SqlDbType.UniqueIdentifier,16) ,            
                        new SqlParameter("@CType", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@ItemCode", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CVersion", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@Classificatiion", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@ItemName", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@Unit", SqlDbType.NVarChar,10) ,            
                        new SqlParameter("@Quantity", SqlDbType.NVarChar,50)             
                        };
                        parameters[0].Value = Guid.NewGuid();
                        parameters[1].Value = item.UnitPrice;
                        parameters[2].Value = item.PromiseDate;
                        parameters[3].Value = item.RequireDate;
                        parameters[4].Value = item.CRemark;
                        parameters[5].Value = new Guid(item.CID);
                        parameters[6].Value = item.CType;
                        parameters[7].Value = item.ItemCode;
                        parameters[8].Value = item.CVersion;
                        parameters[9].Value = item.Classificatiion;
                        parameters[10].Value = item.ItemName;
                        parameters[11].Value = item.Unit;
                        parameters[12].Value = item.Quantity;

                        Dict.Add(keyIndex + "&" + strSql.ToString(), parameters);
                        strSql = new StringBuilder();
                        parameters = null;
                        keyIndex++;
                    }
                }

                //执行数据库操作
                int result = DbHelperSQL.ExecuteSqlListTrans(Dict);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据合同表ID查询产品
        /// </summary>
        /// <param name="cid">合同表ID</param>
        /// <returns></returns>
        public DataTable GetContactProductByCID(string cid)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select row_number() over (order by id) as rowno,(Quantity * convert(decimal(18,4),UnitPrice)) as Amount,* ");
                strSql.Append(" FROM ContactProduct ");
                if (!string.IsNullOrEmpty(cid))
                {
                    strSql.Append(" where CID='" + cid + "'");
                }

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
