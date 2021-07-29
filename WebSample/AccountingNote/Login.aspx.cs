using AccountingNote.DBsourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLogInfo"] != null)
            {
                this.plclogin.Visible = false;
            }
            else
            {
                this.plclogin.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //從資料庫取出的
            string db_Account = "Admin";
            string db_Password = "12345";
            //inp = 輸入值
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPassword.Text;

            //check empty
            if(string.IsNullOrEmpty(inp_Account) || string.IsNullOrEmpty(inp_PWD))
            {
                this.ltlMessage.Text = "Required the Account / PWD";
                return;
            }

            var dr = UserInfoManager.GetUserInfoByAccount(inp_Account);

            //check null
            if (dr == null)
            {
                this.ltlMessage.Text = "This Account doesn't exists";
                return;
            }
            //check Account / Pwd
            //Compare 比對大小寫 , true = 大小寫不干擾 false = 大小寫字串需一致
            if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 && string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0)
            {
                //進入使用者畫面
                this.Session["UserLoginInfo"] = dr["Account"].ToString();
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.ltlMessage.Text = "Login Fail ! Please check your Account / Password";
                return;
            }


        }
    }
}