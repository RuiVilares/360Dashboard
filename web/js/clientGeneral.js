$(document).ready(function(){
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

    $(".melhores_c").html("Melhores Clientes (Valor de Vendas)");

  }});
});
