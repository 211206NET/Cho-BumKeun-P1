using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Customer {
    public Customer(){}
    public Customer(string username)
    {
        this.UserName = username;
    }
    public Customer(DataRow row)
    {
        this.UserName = row["Username"].ToString() ?? "";
        this.Password = row["Password"].ToString() ?? "";
    }
    
    public int Id { get; set; }
    
    [Required]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username can only have alphanumeric characters")]
    public string UserName { get; set; }
    
    [Required]
    [RegularExpression("^[a-zA-Z0-9!?']+$", ErrorMessage = "Password can only have alphanumeric characters, !, ?, and '")]
    public string Password { get; set; }

    //private string _username;
    //private string _password;

    //public string UserName
    //{
    //    get => _username;
    //    set
    //    {
    //        Regex pattern = new Regex("^[a-zA-Z0-9]+$");
    //        if(string.IsNullOrWhiteSpace(value))
    //        {
    //            throw new InputInvalidException("Username can't be empty");
    //        }
    //        else if(!pattern.IsMatch(value))
    //        {
    //            throw new InputInvalidException("Username can only have alphanumeric characters");
    //        }
    //        else
    //        {
    //            this._username = value;
    //        }
    //    }
    //}
    //public string Password
    //{
    //    get => _password;
    //    set
    //    {
    //        Regex pattern = new Regex("^[a-zA-Z0-9!?']+$");
    //        if(string.IsNullOrWhiteSpace(value))
    //        {
    //            throw new InputInvalidException("Password can't be empty");
    //        }
    //        else if(!pattern.IsMatch(value))
    //        {
    //            throw new InputInvalidException("Password can only have alphanumeric characters, !, ?, and '");
    //        }
    //        else
    //        {
    //            this._password = value;
    //        }
    //    }
    //}

    /// <summary>
    /// Takes in UserAccount Table's DataRow and fills the columns with the Customer Instance's info
    /// </summary>
    /// <param name="row">UserAccount Table's DataRow pass by ref</param>
    public void ToDataRow(ref DataRow row)
    {
        row["Username"] = this.UserName;
        row["Password"] = this.Password;
    }
}