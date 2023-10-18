<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="createpost.aspx.cs" Inherits="Forum.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
     .purple-button {
        background-color: aliceblue;
        border-color: mediumpurple;
        color: mediumpurple;
        transition: background-color 0.3s, border-color 0.3s, color 0.3s;
    }

    /* Hover state styles */
    .purple-button:hover {
        background-color: mediumpurple;
        border-color: aliceblue;
    }

    .abc :focus{
        border: solid 2.5px;
        border-color: mediumpurple;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div style="margin: 3% 20% 5% 20%;">
        <h3 class="Title mb-4" style="text-align: center; text-transform: uppercase; margin-bottom: 3%;">Add Post</h3>
        <div class="mb-3 abc">
            <asp:Label runat="server" Text="Label" class="form-label">Title</asp:Label>
            <asp:TextBox runat="server" class="form-control" ID="tbTitle"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Title is Required." ControlToValidate="tbTitle" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3 abc">
            <asp:Label runat="server" Text="Label" class="form-label">Content</asp:Label>
            <asp:TextBox class="form-control" ID="tbContent" runat="server" TextMode="MultiLine" Rows="10" style="width:100%; resize: none"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Content is required." ControlToValidate="tbContent" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
         <div>
            <asp:Label runat="server" Text="Label" class="form-label white-text">Post Picture</asp:Label>
            <asp:FileUpload runat="server" accept="image/*" ID="filePostPic" />
        </div>
        <div style="display:flex; justify-content:center">
            <asp:Button ID="ButtonPost" runat="server" OnClick="ButtonPost_Click" Text="Post" CssClass="btn btn-outline-primary purple-button" class="btn"/>
        </div>
        
        <div class="mb-3">
            <asp:Label ID="lblError" Text="" runat="server" style="color: red;"/>
        </div>
    </div>

</asp:Content>
