using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MembershipManager.Services;
using MembershipManager.Models;
using MembershipManager.Utils;

namespace MembershipManager
{
    public partial class LoginForm : Form
    {
        private MemberService memberService;
        public Member LoggedInMember { get; private set; }   // 추가

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(MemberService service)
        {
            InitializeComponent();
            memberService = service;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputPassword = txtLoginPassword.Text;
            string hashedPassword = HashHelper.ComputeSha256(txtLoginPassword.Text);

            var member = memberService
                .GetMembers()
                .FirstOrDefault(m =>
                    m.Username == txtLoginUsername.Text &&
                    m.Password == hashedPassword);

            if(member != null)
            {
                LoggedInMember = member;  // 로그인 사용자 저장

                MessageBox.Show("로그인 성공");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀립니다.");
            }
        }
    }
}
