using LibraryDataAccessLayer;
using LibraryDTOs;

namespace LibraryBusinessLayer
{
    public class MemberService
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public MemberDTO MDTO
        {
            get
            {
                return (new MemberDTO(this.MemberID, this.NationalID, this.FirstName, this.LastName, this.DateOfBirth, this.DateOfJoin,
                    this.Address, this.Phone));
            }
        }

        public int MemberID { get; set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }

        public MemberService(MemberDTO MDTO, enMode cMode = enMode.AddNew)
        {
            this.MemberID = MDTO.MemberID;
            this.NationalID = MDTO.NationalID;
            this.FirstName = MDTO.FirstName;
            this.LastName = MDTO.LastName;
            this.DateOfBirth = MDTO.DateOfBirth;
            this.Address = MDTO.Address;
            this.Phone = MDTO.Phone;

            Mode = cMode;
        }


        public static MemberService Find(int memberId)
        {
            MemberDTO MDTO = MemberData.GetMemberById(memberId);

            if (MDTO != null)
            {
                return new MemberService(MDTO, enMode.Update);
            }
            else
            {
                return null;
            }

        }

        public static List<MemberDTO> GetAllMembers()
        {
            return MemberData.GetAllMembers();
        }

        private bool _AddNewMember()
        {
            this.MemberID = MemberData.AddNewMember(MDTO);

            return (this.MemberID != -1);
        }

        private bool _UpdateMember()
        {
            return MemberData.UpdateMember(MDTO);
        }

        public static bool DeleteMember(int memberId)
        {
            return MemberData.DeleteMember(memberId);
        }

        public static bool ReactiveMember(int memberId)
        {
            return MemberData.ReactiveMember(memberId);
        }

        public static List<MemberDTO>SearchMember(string query)
        {
            return MemberData.SearchMember(query);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateMember();
            }

            return false;
        }


    }
}
