$(document).ready(function(){
  var ref = getUrlParameter("supplier");

  $.ajax({url: "http://localhost:49822/api/supplier/get_provider_info/", dataType: 'json', success: function(result){
    $(".number").html(result['numFornecedores']);
    $(".pendentes").html(parseFloat(result['valoresPendentes'].toFixed(2)).toLocaleString());
    $(".backlogs").html(parseFloat(result['valoresBacklog'].toFixed(2)).toLocaleString());
  }});
});
