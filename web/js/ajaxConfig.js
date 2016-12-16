function ajaxConfig(){
    $.ajaxSetup({
        type : "POST",
        data : {
            company: $.cookie("cmpny"),
            username : $.cookie("user"),
            password : $.cookie("pass")
        },

        error : function(){
         console.log("ola");//   window.location.replace("login.html?invalidLogin=true");
        }
    });
}
