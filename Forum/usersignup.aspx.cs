using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forum
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // sign up button click event
        protected void UserSignupBtn_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {
                Response.Write("<script>alert('Member Already Exist with this Email ID, try other ID');</script>");
            }
            else
            {   

                signUpNewUser();
                Response.Write("<script>alert('Sign Up Successful.');</script>");
                Response.Redirect("homepage.aspx");
            }
        }

        // user defined method
        bool checkUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

              

                SqlCommand cmd = new SqlCommand("SELECT * from [dbo].[User] where UserEmail='" + tbEmail.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void signUpNewUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                byte[] userPicBytes = null;
                SqlCommand cmd;
                if (fileUserPic.HasFile)
                {
                    userPicBytes = fileUserPic.FileBytes;
                    cmd = new SqlCommand("INSERT INTO [dbo].[User](username,useremail,userpassword,role,UserPic) values(@username,@useremail,@userpassword,@role,@userpic)", con);
                    cmd.Parameters.AddWithValue("@userpic", userPicBytes);
                } else
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[User](username,useremail,userpassword,role) values(@username,@useremail,@userpassword,@role)", con);
                }

                cmd.Parameters.AddWithValue("@username", tbName.Text.Trim());
                cmd.Parameters.AddWithValue("@useremail", tbEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@userpassword", tbPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@role", "user");
                

                cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT * from [dbo].[User] where UserEmail='" + tbEmail.Text.Trim() + "' AND UserPassword='" + tbPassword.Text.Trim() + "';", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["userid"] = dr.GetValue(0).ToString();
                    }
                }
                con.Close();
                Session["username"] = tbName.Text.Trim();
                Session["email"] = tbEmail.Text.Trim();
                Session["role"] = "user";
            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}