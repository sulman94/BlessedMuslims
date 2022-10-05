
var validage = false;

function ValidateDateOfBirth(birthday) {
    debugger
    var birthformat = /(\d+)\-(\d+)\-(\d+)/.exec(birthday)
    birthformat[2] = (Number(birthformat[2]) - 1).toString()
    birthday = new Date(birthformat[1], birthformat[2], birthformat[3]);
    birthday = new Date(birthday); 
    var ageDifMs = Date.now() - birthday.getTime();
    var ageDate = new Date(ageDifMs); 
    console.log(Math.abs(ageDate.getUTCFullYear() - 1970))
    var age = Math.abs(ageDate.getUTCFullYear() - 1970);
    if (age < 18) {
        validage = false;
        //Swal.fire({
        //    text: "Date Of Birth cannot be less than 18 years",
        //    icon: "error",
        //    buttonsStyling: false,
        //    confirmButtonText: "Ok, got it!",
        //    customClass: {
        //        confirmButton: "btn font-weight-bold btn-light"
        //    }
        //})
        //birthday = "Date Of Birth cannot be less than 18 years";
    }
    else {
        validage = true;
    }
    return validage;
    
}