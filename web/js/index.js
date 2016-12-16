$(document).ready(function(){

  ajaxConfig();  

  
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

  $.ajax({url: "http://localhost:49822/api/supplier/get_topf/" + ref, dataType: 'json', success: function(result){

    for(var i = 0; i < result.length && i < 10; i++){
      result[i].valor = result[i].valor.toFixed(2);
    }

    Morris.Bar({
        "element" : "morris-bar-fornecedores",
        "data" : result.slice(0, 10),
        "xkey" : "name",
        "ykeys": ["valor"],
        "labels": ["Compras"],
        "resize" : true
    });

    $(".melhores_f").html("Melhores fornecedores<a href='providerGeneral.html' class='pull-right'>Ver mais</a>");

  }});
});
