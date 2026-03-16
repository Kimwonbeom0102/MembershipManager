using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MembershipManager.Models;
using MembershipManager.Data;
using MembershipManager.Utils;

namespace MembershipManager.Services
{
    /// <summary>
    /// MemberService
    /// 회원 관련 비즈니스 로직 담당
    /// UI에서는 이 클래스를 통해서만 데이터를 다룸
    /// 왜 Service를 만들까?
    /// UI 코드와 데이터 처리 코드를 분리하기 위해
    /// 실무에서는 Service Layer 생성
    /// </summary>
    public class MemberService
    {
        //private MemberStorage storage;
        private List<Member> members;
        private MemberRepository repository;

        // ID 자동 증가용
        private int nextId = 1;

        public MemberService()
        {
            //storage = new MemberStorage();
            //members = storage.GetMembers();

            // Repository 생성
            repository = new MemberRepository();

            // JSON에서 데이터 불러오기
            members = repository.LoadMembers();

            // 기존 데이터가 있다면
            // 마지막 ID 기준으로 nextId 설정
            if (members.Count > 0)
            {
                nextId = members.Max(m => m.Id) + 1;
            }

        }
        
        /// <summary>
        /// 회원 목록 반환
        /// </summary>
        public List<Member> GetMembers()
        {
            return members;
        }

        /// <summary>
        /// 회원 등록
        /// </summary>
        public void AddMember(string name, string phone, string address, string username, string password)
        {
            int newId = members.Count + 1;

            // 새로운 회원 객체 생성
            Member member = new Member()
            {
                Id = nextId++, // 자동 ID 증가
                Name = name,
                Phone = phone,
                Address = address,
                Username = username,

                // 비밀번호 해싱
                Password = HashHelper.ComputeSha256(password)
            };

            members.Add(member);
            // JSON 저장
            repository.SaveMembers(members);
        }

        /// <summary>
        /// 회원 수정
        /// </summary>
        public void UpdateMember(int id, string name, string phone, string address, string username, string password)
        {
            // 수정할 회원 찾기
            var member = members.FirstOrDefault(m => m.Id == id);

            if (member == null)
                return;

            // 값 수정
            member.Name = name;
            member.Phone = phone;
            member.Address = address;
            member.Username = username;
            member.Password = password;

            // JSON 다시 저장
            repository.SaveMembers(members);
        }

        /// <summary>
        /// 회원 삭제
        /// </summary>
        public void DeleteMember(int id)
        {
            var member = members.FirstOrDefault(m => m.Id == id);

            if (member != null)
            {
                members.Remove(member);
            }

            // JSON 저장
            repository.SaveMembers(members);
        }

        /// <summary>
        /// 회원 검색
        /// </summary>
        public List<Member> SearchMembers(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return members;

            return members
                .Where(m =>
                    m.Name.ToLower().Contains(keyword.ToLower()) ||
                    m.Username.ToLower().Contains(keyword.ToLower()))
                .ToList();
        }

        public bool IsUsernameDuplicate(string username)
        {
            return members.Any(m => m.Username == username);
        }

    }
}
