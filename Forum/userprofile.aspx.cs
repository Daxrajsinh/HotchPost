using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Forum
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["role"] == null)
            {
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                if(!IsPostBack)
                {
                    lbluser.Text = Session["username"].ToString();
                    lbluseremail.Text = Session["email"].ToString();
                    tbName.Text = Session["username"].ToString();
                    DisplayUserProfilePic();
                }
            }
        }
        void DisplayUserProfilePic()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                int userId = Convert.ToInt32(Session["userid"]);
                SqlCommand cmd = new SqlCommand("SELECT UserPic FROM [User] WHERE Id = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                byte[] userPicBytes = (byte[])cmd.ExecuteScalar();

                // Display the fetched image
                if (userPicBytes != null && userPicBytes.Length > 0)
                {
                    string base64Image = Convert.ToBase64String(userPicBytes);
                    imgUserPic.ImageUrl = "data:image/jpeg;base64," + base64Image;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updateprofile_Click(object sender, EventArgs e)
        {
            Updateuserprofile();
        }

       

        // Method to update user's profile picture
       

        void Updateuserprofile()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                byte[] userPicBytes = null;
               

                SqlCommand cmd1 = new SqlCommand("SELECT * from [dbo].[User] where id="+ Session["userid"] +" AND userpassword='" + oldpass.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[User] SET username='" + tbName.Text.Trim() + "', userpassword='" + newpass.Text.Trim() + "' WHERE id = " + Session["userid"] + ";", con);
                    
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session.Abandon();
                    Response.Write("<script>alert('Profile updated successfully.');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid credentials');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}