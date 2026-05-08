namespace LibraryDTOs
{
    public class MemberDTO
    {

        public MemberDTO(int memberid, string nationalid, string firstname, string lastname,
            DateTime dateofbirth, DateTime dateofjoin, string address, string phone)
        {
            this.MemberID = memberid;
            this.NationalID = nationalid;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.DateOfBirth = dateofbirth;
            this.DateOfJoin = dateofjoin;
            this.Address = address;
            this.Phone = phone;
        }
        public int MemberID { get; set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }

    public class CreateMemberDto
    {
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }

    public class UpdateMemberDto
    {

        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }

}
