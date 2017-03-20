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
    /// <summary>
    /// 合同模板DAL
    /// </summary>
    public class ContactTempletDAL
    {
        /// <summary>
        /// 获取采购组织信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPRProject()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ProjectNr,Description from PRProject ");

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 动态添加查询参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlParameter[] SetSqlParamenter(ref StringBuilder strSql, ContactTemplet model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;

                if (!string.IsNullOrEmpty(model.ID))
                {
                    strSql.Append(" and ID = @ID ");
                    para = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16);
                    para.Value = new Guid(model.ID);
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTName))
                {
                    strSql.Append(" and CTName = @CTName ");
                    para = new SqlParameter("@CTName", SqlDbType.NVarChar, 100);
                    para.Value = model.CTName;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBusiness))
                {
                    strSql.Append(" and CTBusiness = @CTBusiness ");
                    para = new SqlParameter("@CTBusiness", SqlDbType.NVarChar, 20);
                    para.Value = model.CTName;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTCode))
                {
                    strSql.Append(" and CTCode = @CTCode ");
                    para = new SqlParameter("@CTCode", SqlDbType.NVarChar, 20);
                    para.Value = model.CTCode;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTVersion))
                {
                    strSql.Append(" and CTVersion = @CTVersion ");
                    para = new SqlParameter("@CTVersion", SqlDbType.NVarChar, 20);
                    para.Value = model.CTVersion;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.ProjectNr))
                {
                    strSql.Append(" and ProjectNr = @ProjectNr ");
                    para = new SqlParameter("@ProjectNr", SqlDbType.NVarChar, 20);
                    para.Value = model.ProjectNr;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTPurchaseOrg))
                {
                    strSql.Append(" and CTPurchaseOrg = @CTPurchaseOrg ");
                    para = new SqlParameter("@CTPurchaseOrg", SqlDbType.NVarChar, 100);
                    para.Value = model.CTPurchaseOrg;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBuyer))
                {
                    strSql.Append(" and CTBuyer = @CTBuyer ");
                    para = new SqlParameter("@CTBuyer", SqlDbType.NVarChar, 20);
                    para.Value = model.CTBuyer;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTSupplier))
                {
                    strSql.Append(" and CTSupplier = @CTSupplier ");
                    para = new SqlParameter("@CTSupplier", SqlDbType.NVarChar, 100);
                    para.Value = model.CTSupplier;
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
        /// 动态添加查询参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlParameter[] SetSqlParamenter2(ref StringBuilder strSql, ContactTemplet model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;

                if (!string.IsNullOrEmpty(model.CTName))
                {
                    strSql.Append(" and (CTName like @CTName ");
                    para = new SqlParameter("@CTName", SqlDbType.NVarChar, 100);
                    para.Value = "%" + model.CTName + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBusiness))
                {
                    strSql.Append(" or CTBusiness like @CTBusiness ");
                    para = new SqlParameter("@CTBusiness", SqlDbType.NVarChar, 20);
                    para.Value = "%" + model.CTBusiness + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTCode))
                {
                    strSql.Append(" or CTCode like @CTCode ");
                    para = new SqlParameter("@CTCode", SqlDbType.NVarChar, 20);
                    para.Value = "%" + model.CTCode + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTVersion))
                {
                    strSql.Append(" or CTVersion like @CTVersion ");
                    para = new SqlParameter("@CTVersion", SqlDbType.NVarChar, 20);
                    para.Value = "%" + model.CTVersion + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.ProjectNr))
                {
                    strSql.Append(" or ProjectNr like @ProjectNr ");
                    para = new SqlParameter("@ProjectNr", SqlDbType.NVarChar, 20);
                    para.Value = "%" + model.ProjectNr + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTPurchaseOrg))
                {
                    strSql.Append(" or CTPurchaseOrg like @CTPurchaseOrg ");
                    para = new SqlParameter("@CTPurchaseOrg", SqlDbType.NVarChar, 100);
                    para.Value = "%" + model.CTPurchaseOrg + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBuyer))
                {
                    strSql.Append(" or CTBuyer like @CTBuyer ");
                    para = new SqlParameter("@CTBuyer", SqlDbType.NVarChar, 20);
                    para.Value = "%" + model.CTBuyer + "%";
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTSupplier))
                {
                    strSql.Append(" or CTSupplier like @CTSupplier ");
                    para = new SqlParameter("@CTSupplier", SqlDbType.NVarChar, 100);
                    para.Value = "%" + model.CTSupplier + "%";
                    splist.Add(para);
                }
                strSql.Append(") ");
            }

            SqlParameter[] param = null;
            if (splist.Count > 0)
            {
                param = splist.ToArray();
            }
            return param;
        }

        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="ctCode">编号</param>
        /// <param name="id">id号，如果新建则为空</param>
        /// <returns></returns>
        public bool ExistsCTCode(string ctCode, string id)
        {
            try
            {
                //编号为空，则直接返回存在
                if (string.IsNullOrEmpty(ctCode))
                {
                    return true;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id from ContactTemplet where CTCode='" + ctCode + "' ");
                if (!string.IsNullOrEmpty(id))
                {
                    strSql.Append(" and ID<>'" + new Guid(id) + "'");
                }

                bool rows = DbHelperSQL.Exists(strSql.ToString());
                return rows;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="ctName">名称</param>
        /// <param name="id">id号，如果新建则为空</param>
        /// <returns></returns>
        public bool ExistsCTName(string ctName, string id)
        {
            try
            {
                //名称为空，则直接返回存在
                if (string.IsNullOrEmpty(ctName))
                {
                    return true;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id from ContactTemplet where CTName='" + ctName + "' ");
                if (!string.IsNullOrEmpty(id))
                {
                    strSql.Append(" and ID<>'" + new Guid(id) + "'");
                }

                bool rows = DbHelperSQL.Exists(strSql.ToString());
                return rows;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        /// <summary>
        /// 获取模板编号
        /// </summary>
        /// <returns></returns>
        public string GetCTCode(string projectNr)
        {
            string ctCode = "";
            StringBuilder strSql = new StringBuilder();//SQL语句
            //strSql.Append("select (min(ProjectNr)+'-'+");
            //strSql.Append("(case when len(Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1))=1 ");
            //strSql.Append(" then '0'+Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1) ");
            //strSql.Append(" else Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1) end)");
            //strSql.Append(" ) CTCode ");
            //strSql.Append(" from ContactTemplet where ProjectNr='" + projectNr + "' ");

            strSql.Append("select ");
            //strSql.Append("(case when len(Convert(char,max(CTCode))+1)=1 ");
            //strSql.Append(" then '0'+Convert(char,max(CTCode))+1 ");
            //strSql.Append(" else Convert(char,max(CTCode))+1 end) CTCode");

            strSql.Append(" (Convert(char,max(CTCode))+1) CTCode ");
            strSql.Append(" from ContactTemplet where ProjectNr='" + projectNr + "' ");

            DataTable dt = null;
            //返回多表查询结果
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    ctCode = dt.Rows[0][0].ToString();
                }
            }
            if (string.IsNullOrEmpty(ctCode))
            {
                //ctCode = projectNr + "-01";
                ctCode = "1";
            }
            return ctCode;
        }

        /// <summary>
        /// 获取模板版本号
        /// </summary>
        /// <returns></returns>
        public string GetCTVersion(string ctName)
        {
            string ctVersion = "";
            StringBuilder strSql = new StringBuilder();//SQL语句
            strSql.Append("select ");
            //strSql.Append("(case when len(Convert(char,max(CTVersion))+1)=1 ");
            //strSql.Append(" then '0'+Convert(char,max(CTVersion))+1 ");
            //strSql.Append(" else Convert(char,max(CTVersion))+1 end) CTVersion");

            strSql.Append(" (Convert(char,max(CTVersion))+1) CTVersio ");
            strSql.Append(" from ContactTemplet where CTName='" + ctName + "' ");

            DataTable dt = null;
            //返回多表查询结果
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    ctVersion = dt.Rows[0][0].ToString();
                }
            }
            if (string.IsNullOrEmpty(ctVersion))
            {
                ctVersion = "1";
            }
            return ctVersion;
        }

        /// <summary>
        /// 保存合同模板
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int AddContactTemplet(ContactTemplet model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("insert into ContactTemplet(");
                strSql.Append("ID,CTCreate,CTCreateTime,CTStatus,IsDefault,CTCode,CTVersion,CTName,CTBusiness,CTPurchaseOrg,CTBuyer,CTSupplier,CTRemark,ProjectNr,Parameter");
                strSql.Append(") values (");
                strSql.Append("@ID,@CTCreate,@CTCreateTime,@CTStatus,@IsDefault,");

                string ctCode = GetCTCode(model.ProjectNr);
                //if (string.IsNullOrEmpty(ctCode))
                //{
                strSql.Append(" '" + ctCode + "', ");
                //}
                //else
                //{
                //    //编号计算
                //strSql.Append("select (ProjectNr+'-'+");
                //strSql.Append("(case when len(Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1))=1 ");
                //strSql.Append(" then '0'+Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1) ");
                //strSql.Append(" else Convert(char,right(max(CTCode),len(max(CTCode))- charindex('-',max(CTCode)))+1) end)");
                //strSql.Append(" ) CTCode ");
                //strSql.Append(" from ContactTemplet where ProjectNr='" + model.ProjectNr + "' group by ProjectNr ");
                //}

                string ctVersion = GetCTVersion(model.CTName);
                strSql.Append(" '" + ctVersion + "', ");

                strSql.Append(" @CTName,@CTBusiness,@CTPurchaseOrg,@CTBuyer,@CTSupplier,@CTRemark,@ProjectNr,@Parameter");
                strSql.Append(") ");

                SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16) ,
                        new SqlParameter("@CTCreate", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CTCreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CTStatus", SqlDbType.Char,1) ,            
                        new SqlParameter("@IsDefault", SqlDbType.Char,1) ,            
                        new SqlParameter("@CTCode", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CTVersion", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CTName", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CTBusiness", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@CTPurchaseOrg", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CTBuyer", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CTSupplier", SqlDbType.NVarChar,100) ,            
                        new SqlParameter("@CTRemark", SqlDbType.NText),
                        new SqlParameter("@ProjectNr", SqlDbType.NVarChar,20) , 
                        new SqlParameter("@Parameter", SqlDbType.NText)
                };

                parameters[0].Value = Guid.NewGuid();
                parameters[1].Value = model.CTCreate;
                parameters[2].Value = model.CTCreateTime;
                parameters[3].Value = model.CTStatus;
                parameters[4].Value = model.IsDefault;
                parameters[5].Value = model.CTCode;
                parameters[6].Value = model.CTVersion;
                parameters[7].Value = model.CTName;
                parameters[8].Value = model.CTBusiness;
                parameters[9].Value = model.CTPurchaseOrg;
                parameters[10].Value = model.CTBuyer;
                parameters[11].Value = model.CTSupplier;
                parameters[12].Value = model.CTRemark;
                parameters[13].Value = model.ProjectNr;
                parameters[14].Value = model.Parameter;

                //执行数据库操作
                int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询合同模板【适用下拉选项】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetContactTempletOption(ContactTemplet model)
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

                strSql.Append("select ID,CTName ");
                strSql.Append(" from ContactTemplet ");
                strSql.Append(" where 1=1 ");
                strSql.Append(whereSql.ToString());

                DataTable dt = null;
                //返回多表查询结果
                DataSet ds = DbHelperSQL.Query(strSql.ToString(), param);
                if (ds != null && ds.Tables.Count > 0)
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
        /// 查询合同模板(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetContactTemplet(ContactTemplet model, int pageSize, int pageIndex, ref int total)
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

                strSql.Append("select top " + pageSize + " (A.ProjectNr+'-'+A.CTCode) as Code,  A.* ");
                strSql.Append(" from(");
                strSql.Append("select top " + (pageSize * pageIndex) + " ROW_NUMBER() over(order by CTCreateTime desc) as rowno,* ");

                strSql.Append(" from ContactTemplet ");
                strSql.Append(" where 1=1 ");
                strSql.Append(whereSql.ToString());
                strSql.Append(" )as A ");
                //分页
                strSql.Append(" where A.rowno>" + (pageSize * (pageIndex - 1)));

                //查询总条数条件
                StringBuilder totalStr = new StringBuilder();
                totalStr.Append("select count(ID) FROM ContactTemplet with(nolock) where 1=1 ");
                totalStr.Append(whereSql.ToString());

                total = 0;
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
        /// 查询合同模板【模糊查询】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetFuzzyQuery(ContactTemplet model, int pageSize, int pageIndex, ref int total)
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

                strSql.Append("select top " + pageSize + " (A.ProjectNr+'-'+A.CTCode) as Code,  A.* ");
                strSql.Append(" from(");
                strSql.Append("select top " + (pageSize * pageIndex) + " ROW_NUMBER() over(order by CTCreateTime desc) as rowno,* ");

                strSql.Append(" from ContactTemplet ");
                strSql.Append(" where 1=1 ");
                strSql.Append(whereSql.ToString());
                strSql.Append(" )as A ");
                //分页
                strSql.Append(" where A.rowno>" + (pageSize * (pageIndex - 1)));

                //查询总条数条件
                StringBuilder totalStr = new StringBuilder();
                totalStr.Append("select count(ID) FROM ContactTemplet with(nolock) where 1=1 ");
                totalStr.Append(whereSql.ToString());

                total = 0;
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
        /// 更新合同模板信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateContactTemplet(ContactTemplet model)
        {
            //Dictionary<string, SqlParameter[]> Dict = new Dictionary<string, SqlParameter[]>();
            //int keyIndex = 0;
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("update ContactTemplet set ");

                SqlParameter[] param = null;
                //动态添加参数
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.ID.ToString())) { return 0; }
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
        /// 更新合同模板信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public int UpdateContactTemplet(IList<ContactTemplet> modelList)
        {
            try
            {
                Dictionary<string, SqlParameter[]> Dict = new Dictionary<string, SqlParameter[]>();
                int keyIndex = 0;
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] param = null;
                foreach (ContactTemplet item in modelList)
                {
                    strSql.Append("update ContactTemplet set ");

                    //动态添加参数
                    if (item != null)
                    {
                        if (string.IsNullOrEmpty(item.ID.ToString())) { return 0; }
                        param = SetUpdataSqlParamenter(ref strSql, item);

                        Dict.Add(keyIndex + "&" + strSql.ToString(), param);
                        strSql = new StringBuilder();
                        param = null;
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
        /// 复制模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns></returns>
        public int CopyContractTempleet(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return 0;
                }

                StringBuilder strSql = new StringBuilder();//SQL语句

                strSql.Append("insert into ContactTemplet(");
                strSql.Append("CTCreate,CTCreateTime,CTStatus,IsDefault,CTCode,CTVersion,CTName,CTBusiness,CTPurchaseOrg,CTBuyer,CTSupplier,CTRemark,ProjectNr");
                strSql.Append(")");

                strSql.Append(" select CTCreate,getdate(),CTStatus,'0',");
                strSql.Append(" (select  Convert(char,max(c.CTCode))+1 from ContactTemplet c where c.ProjectNr=ct.ProjectNr),");
                strSql.Append(" (select  Convert(char,max(c.CTVersion))+1 from ContactTemplet c where c.CTName=ct.CTName),");
                strSql.Append(" CTName,CTBusiness,CTPurchaseOrg,CTBuyer,CTSupplier,CTRemark,ProjectNr ");
                strSql.Append("  from ContactTemplet ct where id='" + new Guid(id) + "';");

                int relt = DbHelperSQL.ExecuteSql(strSql.ToString());
                return relt;
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
        private SqlParameter[] SetUpdataSqlParamenter(ref StringBuilder strSql, ContactTemplet model)
        {
            //动态添加参数
            List<SqlParameter> splist = new List<SqlParameter>();
            if (model != null)
            {
                SqlParameter para = null;
                if (!string.IsNullOrEmpty(model.CTStatus))
                {
                    strSql.Append(" CTStatus = @CTStatus ");
                    para = new SqlParameter("@CTStatus", SqlDbType.Char, 1);
                    para.Value = model.CTStatus;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTCode))
                {
                    strSql.Append(" ,CTCode = @CTCode ");
                    para = new SqlParameter("@CTCode", SqlDbType.NVarChar, 20);
                    para.Value = (string)model.CTCode.Split('-')[1];
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTVersion))
                {
                    strSql.Append(" ,CTVersion = @CTVersion ");
                    para = new SqlParameter("@CTVersion", SqlDbType.NVarChar, 20);
                    para.Value = model.CTVersion;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTName))
                {
                    strSql.Append(" ,CTName = @CTName ");
                    para = new SqlParameter("@CTName", SqlDbType.NVarChar, 100);
                    para.Value = model.CTName;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBusiness))
                {
                    strSql.Append(" ,CTBusiness = @CTBusiness ");
                    para = new SqlParameter("@CTBusiness", SqlDbType.NVarChar, 20);
                    para.Value = model.CTBusiness;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTPurchaseOrg))
                {
                    strSql.Append(" ,CTPurchaseOrg = @CTPurchaseOrg ");
                    para = new SqlParameter("@CTPurchaseOrg", SqlDbType.NVarChar, 100);
                    para.Value = model.CTPurchaseOrg;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTBuyer))
                {
                    strSql.Append(" ,CTBuyer = @CTBuyer ");
                    para = new SqlParameter("@CTBuyer", SqlDbType.NVarChar, 100);
                    para.Value = model.CTBuyer;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTSupplier))
                {
                    strSql.Append(" ,CTSupplier = @CTSupplier ");
                    para = new SqlParameter("@CTSupplier", SqlDbType.NVarChar, 100);
                    para.Value = model.CTSupplier;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.CTRemark))
                {
                    strSql.Append(" ,CTRemark = @CTRemark ");
                    para = new SqlParameter("@CTRemark", SqlDbType.NText);
                    para.Value = model.CTRemark;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.IsDefault))
                {
                    strSql.Append(" ,IsDefault = @IsDefault ");
                    para = new SqlParameter("@IsDefault", SqlDbType.VarChar, 1);
                    para.Value = model.IsDefault;
                    splist.Add(para);
                }
                if (!string.IsNullOrEmpty(model.ProjectNr))
                {
                    strSql.Append(" ,ProjectNr = @ProjectNr ");
                    para = new SqlParameter("@ProjectNr", SqlDbType.NVarChar, 20);
                    para.Value = model.ProjectNr;
                    splist.Add(para);
                }

                strSql.Append(" where ID=@ID ");
                para = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16);
                para.Value = new Guid(model.ID);
                splist.Add(para);
            }

            SqlParameter[] param = null;
            if (splist.Count > 0)
            {
                param = splist.ToArray();
            }
            return param;
        }

    }
}
