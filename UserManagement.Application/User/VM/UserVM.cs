namespace UserManagement.Application.User.VM
{
    using System.Collections.Generic;
    using UserManagement.Application.User.DTO;
    public class UserVM
    {
        public IList<UserDTO> UserList { get; set; }
    }
}
