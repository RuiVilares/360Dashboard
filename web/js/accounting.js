$(document).ready(function(){
  $.ajax({url: "http://localhost:49822/api/accounting/getAtivos_Passivos/", dataType: 'json', success: function(result){
      $(".ativos").html(result.m_Item1.toFixed(3) + " €");
      $(".passivos").html(-result.m_Item2.toFixed(3) + " €");
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getBalancete/", dataType: 'json', success: function(result){
      for(var i = 0; i < result.length; i++){
          var ob = result[i];

          $(".balanceteItems").append('<tr>');
          $(".balanceteItems").append('<td>' + ob.codConta + '</td>');
          $(".balanceteItems").append('<td>' + ob.nomeConta + '</td>');
          $(".balanceteItems").append('<td>' + ob.credito + '</td>');
          $(".balanceteItems").append('<td>' + ob.debito + '</td>');
          $(".balanceteItems").append('<td>' + ob.saldo + '</td>');
          $(".balanceteItems").append('</tr>');

          $(".loading").addClass('hidden');
      }
  }});
});
