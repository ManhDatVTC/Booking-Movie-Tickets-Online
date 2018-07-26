using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persitence;

namespace BL {
    public class Customer_Bl {
        // private Custome_DAL idal = new Custome_DAL ();
        public Customer Login (String email, String password) {
            Custome_DAL idal = new Custome_DAL ();
            string regexEmail = @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            if (Regex.IsMatch (email, regexEmail) != true || email == "" || Regex.IsMatch (password, regexPassword) != true || password == "") {
                return null;
            }
            return idal.Login (email, password);
        }
    }
}