$(document).ready(function(){
  $.ajaxSetup({
      type : "POST",
      data : {
          username : $.cookie("user"),
          password : $.cookie("pass")
      },
      
      error : function(){
          window.location.replace("login.html?invalidLogin=true");
      }
  }); 
  
    var ref = getUrlParameter("product");
  $.ajax({url: "http://localhost:49822/api/clientes/Get_top10c/" + ref, dataType: 'json', success: function(result){
    Morris.Bar({
        "element" : "morris-bar-chart",
        "data" : result.slice(0, 10),
        "xkey" : "name",
        "ykeys": ["valor"],
        "labels": ["Vendas"],
        "resize" : true
    });

    $(".melhores_c").html("Melhores Clientes<a href='clientGeneral.html' class='pull-right'>Ver mais</a>");
  }});
  // ESCREVER AJAX CALL
  $(".melhores_f").html("Melhores Fornecedores<a href='providerGeneral.html' class='pull-right'>Ver mais</a>");
});
