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

  $.ajax({url: "http://localhost:49822/api/accounting/getBalanco/", dataType: 'json', success: function(result){

        var anc = 0;
        for(var i = 0; i < result[0].length; i++){
            anc += parseInt(result[0][i]['m_Item2']);
        }
        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Activos não correntes</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  anc + '</td>');
        $(".balancoItems").append('</tr>');
    
    for (var i = 0; i < result[0].length; i++){
      if(result[0][i]['m_Item2'] != 0){
          $(".balancoItems").append('<tr>');
          $(".balancoItems").append('<td>' + result[0][i]["m_Item1"] + '</td>');
          $(".balancoItems").append('<td>' + result[0][i]["m_Item2"] + '</td>');
          $(".balancoItems").append('</tr>');
        }
    }

        var ac = 0;
        for(var i = 0; i < result[1].length; i++){
            ac += parseInt(result[1][i]['m_Item2']);
        }
        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Activos correntes</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  ac + '</td>');
        $(".balancoItems").append('</tr>');
    
    for (var i = 0; i < result[1].length; i++){
        if(result[1][i]['m_Item2'] != 0){
          $(".balancoItems").append('<tr>');
          $(".balancoItems").append('<td>' + result[1][i]["m_Item1"] + '</td>');
          $(".balancoItems").append('<td>' + result[1][i]["m_Item2"] +'€'+ '</td>');
          $(".balancoItems").append('</tr>');          
      }
    }

        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Total do ativo</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  (anc + ac) + '</td>');
        $(".balancoItems").append('</tr>');

        var cp = 0;
        for(i = 0; i < result[2].length; i++){
            cp += parseInt(result[2][i]['m_Item2']);
        }
        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Capital próprio</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  cp + '</td>');
        $(".balancoItems").append('</tr>');

      for (i = 0; i < result[2].length; i++){
        if(result[2][i]['m_Item2'] !== 0){
          $(".balancoItems").append('<tr>');
          $(".balancoItems").append('<td>' + result[2][i]["m_Item1"] + '</td>');
          $(".balancoItems").append('<td>' + result[2][i]["m_Item2"] +'€'+ '</td>');
          $(".balancoItems").append('</tr>');          
        }
    }


      var pnc = 0;
        for(i = 0; i < result[3].length; i++){
            pnc += parseInt(result[3][i]['m_Item2']);
        }
        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Passivos não correntes</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  pnc + '</td>');
        $(".balancoItems").append('</tr>');

      for (i = 0; i < result[3].length; i++){
        if(result[3][i]['m_Item2'] !== 0){
          $(".balancoItems").append('<tr>');
          $(".balancoItems").append('<td>' + result[3][i]["m_Item1"] + '</td>');
          $(".balancoItems").append('<td>' + result[3][i]["m_Item2"] +'€'+ '</td>');
          $(".balancoItems").append('</tr>');          
      }
    }

      var pc = 0;
        for(i = 0; i < result[4].length; i++){
            pc += parseInt(result[4][i]['m_Item2']);
        }
        $(".balancoItems").append('<tr>');
        $(".balancoItems").append('<td>' + '<strong>Passivos correntes</strong>' + '</td>');
        $(".balancoItems").append('<td>' +  pc + '</td>');
        $(".balancoItems").append('</tr>');

      for (i = 0; i < result[4].length; i++){
        if(result[4][i]['m_Item2'] !== 0){
          $(".balancoItems").append('<tr>');
          $(".balancoItems").append('<td>' + result[4][i]["m_Item1"] + '</td>');
          $(".balancoItems").append('<td>' + result[4][i]["m_Item2"] +'€'+ '</td>');
          $(".balancoItems").append('</tr>');          
      }
    }

     $(".balancoItems").append('<tr>');
     $(".balancoItems").append('<td>' + '<strong>Total do passivo</strong>' + '</td>');
     $(".balancoItems").append('<td>' +  (pnc+pc) + '</td>');
     $(".balancoItems").append('</tr>');

     $(".balancoItems").append('<tr>');
     $(".balancoItems").append('<td>' + '<strong>Total do passivo e capital próprio</strong>' + '</td>');
     $(".balancoItems").append('<td>' +  (pnc+pc+cp) + '</td>');
     $(".balancoItems").append('</tr>');


  }});
});
