$(document).ready(function(){
    ajaxConfig();

    var ref = getUrlParameter("supplier");


  $.ajax({url: "http://localhost:49822/api/supplier/get_topf/" + ref, dataType: 'json', success: function(result){

    for(var i = 0; i < result.length && i < 10; i++){
      result[i].valor = result[i].valor.toFixed(2);
    }

    Morris.Bar({
        "element" : "morris-bar-chart",
        "data" : result.slice(0, 10),
        "xkey" : "name",
        "ykeys": ["valor"],
        "labels": ["Compras"],
        "resize" : true
    });

    $(".melhores_f").html("Melhores fornecedores");

  }});

  $.ajax({url: "http://localhost:49822/api/supplier/get_toppendentes/" + ref, dataType: 'json', success: function(result){

    var top10 = result.sort(GetSortOrder("m_Item3"));
    for(var i = 0; i < top10.length && i < 10; i++){
      top10[i].m_Item3 = result[i].m_Item3.toFixed(2);
    }

    Morris.Bar({
        "element" : "morris-bar-pendente",
        "data" : top10.slice(0, 10),
        "xkey" : "m_Item2",
        "ykeys": ["m_Item3"],
        "labels": ["Valor pendente"],
        "resize" : true
    });

    $(".top_pendentes").html("Fornecedores com maior valor pendente");

  }});

  $.ajax({url: "http://localhost:49822/api/supplier/get_provider_info/", dataType: 'json', success: function(result){
    $(".number").html(result['numFornecedores']);
    $(".pendentes").html(parseFloat(result['valoresPendentes'].toFixed(2)).toLocaleString());
    $(".backlogs").html(parseFloat(result['valoresBacklog'].toFixed(2)).toLocaleString());
  }});

  $(".in").removeClass("in");

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
