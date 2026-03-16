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

namespace MembershipManager
{
    public partial class MainForm : Form
    {
        // 서비스 객체
        private MemberService memberService;

        public MainForm(MemberService service)
        {
            InitializeComponent();

            // 서비스 생성
            memberService = service;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        /// <summary>
        /// DataGridView 갱신
        /// </summary>
        private void RefreshGrid()
        {
            dataGridMembers.DataSource = null;
            dataGridMembers.DataSource = memberService.GetMembers();
            dataGridMembers.Columns["Role"].Visible = false;
            dataGridMembers.Columns["Role"].DefaultCellStyle.NullValue = "";
        }
        
        /// <summary>
        /// 검색
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = memberService.SearchMembers(txtSearch.Text);

            dataGridMembers.DataSource = null;
            dataGridMembers.DataSource = result;
        }

        /// <summary>
        /// 회원 등록 버튼
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 필수값 검사
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("필수 항목을 입력하세요.");
                return;
            }

            memberService.AddMember(
                txtName.Text,
                txtPhone.Text,
                txtAddress.Text,
                txtUsername.Text,
                txtPassword.Text
            );

            RefreshGrid();
        }

        /// <summary>
        /// 회원 삭제
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridMembers.CurrentRow == null)
                return;

            int id = (int)dataGridMembers.CurrentRow.Cells["Id"].Value;

            memberService.DeleteMember(id);

            RefreshGrid();
        }

        private void dataGridMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 선택된 행이 없으면 종료
            if (dataGridMembers.CurrentRow == null)
                return;

            // DataGridView → TextBox 값 전달
            txtName.Text = dataGridMembers.CurrentRow.Cells["Name"].Value.ToString();
            txtPhone.Text = dataGridMembers.CurrentRow.Cells["Phone"].Value.ToString();
            txtAddress.Text = dataGridMembers.CurrentRow.Cells["Address"].Value.ToString();
            txtUsername.Text = dataGridMembers.CurrentRow.Cells["Username"].Value.ToString();
            txtPassword.Text = dataGridMembers.CurrentRow.Cells["Password"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridMembers.CurrentRow == null)
                return;

            int id = (int)dataGridMembers.CurrentRow.Cells["Id"].Value;

            memberService.UpdateMember(
                id,
                txtName.Text,
                txtPhone.Text,
                txtAddress.Text,
                txtUsername.Text,
                txtPassword.Text
            );

            RefreshGrid();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}
