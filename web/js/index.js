$(document).ready(function(){

  ajaxConfig();


    var ref = getUrlParameter("product");
  $.ajax({url: "http://localhost:49822/api/clientes/Get_top10c/" + ref, dataType: 'json', success: function(result){

    Morris.Bar({
        "element" : "morris-bar-chart",
        "data" : result.sort(GetSortOrder("valor")).slice(0, 10),
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

  $.ajax({url: "http://localhost:49822/api/accounting/getDemoResultados/", dataType: 'json', success: function(result){
    var receita = 0.0;
    var despesa = 0.0;

    for (var i = 0; i < result[0].length; i++){
      if(result[0][i].m_Item2 >= 0){
        receita += result[0][i].m_Item2;
      }
      else{
        despesa += result[0][i].m_Item2;
      }
    }

    $("#receita").html(parseFloat(receita.toFixed(2)).toLocaleString());
    $("#despesa").html(parseFloat(despesa.toFixed(2)).toLocaleString());
    $("#lucro").html(parseFloat((receita+despesa).toFixed(2)).toLocaleString());
  }});

});

function GetSortOrder(prop) {
    return function(a, b) {
        if (a[prop] < b[prop]) {
            return 1;
        } else if (a[prop] > b[prop]) {
            return -1;
        }
        return 0;
    }
}
