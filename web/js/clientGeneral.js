$(document).ready(function(){
  var ref = getUrlParameter("product");

  $.ajax({url: "http://localhost:49822/api/clientes/Get_top10c/" + ref, dataType: 'json', success: function(result){

    for(var i = 0; result.length && i < 10; i++){
      result[i].valor = result[i].valor.toFixed(2);
    }

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

  $.ajax({url: "http://localhost:49822/api/clientes/get_top10divida/" + ref, dataType: 'json', success: function(result){

    var top10 = result.sort(GetSortOrder("m_Item3"));
    for(var i = 0; i < top10.length && i < 10; i++){
      top10[i].m_Item3 = result[i].m_Item3.toFixed(2);
    }

    Morris.Bar({
        "element" : "morris-bar-divida",
        "data" : top10.slice(0, 10),
        "xkey" : "m_Item2",
        "ykeys": ["m_Item3"],
        "labels": ["Valor em aberto"],
        "resize" : true
    });

    $(".maior_divida").html("Clientes com maior divida");

  }});

  $.ajax({url: "http://localhost:49822/api/clientes/get_client_info/", dataType: 'json', success: function(result){
    $(".clients_nr").html(result['numClientes']);
    $(".open_value").html(parseFloat(result['valorAberto'].toFixed(2)).toLocaleString());
    $(".billed_value").html(parseFloat(result['valorFaturado'].toFixed(2)).toLocaleString());

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
