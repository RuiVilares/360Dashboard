$(document).ready(function(){

  $.ajax({url: "http://localhost:49822/api/accounting/getAtivos_Passivos/", dataType: 'json', success: function(result){
      $(".ativos").html(result.m_Item1.toFixed(3) + " €");
      $(".passivos").html(-result.m_Item2.toFixed(3) + " €");
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getBalancete/", dataType: 'json', success: function(result){
      for(var i = 0; i < result.length; i++){
          var ob = result[i];

          if (i < 10){
            $(".balanceteItems").append('<tr><td>' + ob.codConta + '</td>' +
            "<td>" + ob.nomeConta + '</td>' +
            "<td class='text-right'>" + ob.credito.toFixed(2) + '</td>' +
            "<td class='text-right'>" + ob.debito.toFixed(2) + '</td>' +
            "<td class='text-right'>" + ob.saldo.toFixed(2) + '</td></tr>');
          }
          else {
            $(".balanceteItems").append("<tr class='collapse balancete'><td>" + ob.codConta + '</td>' +
            "<td>" + ob.nomeConta + '</td>' +
            "<td class='text-right'>" + ob.credito.toFixed(2) + '</td>' +
            "<td class='text-right'>" + ob.debito.toFixed(2) + '</td>' +
            "<td class='text-right'>" + ob.saldo.toFixed(2) + '</td></tr>');
          }

          $(".loading2").addClass('hidden');

      }
      if (result.length >= 10){
        $("#seeMore").html("<a class='clickable pull-right btn' data-toggle='collapse' id='balancete' data-target='.balancete'><span class='glyphicon glyphicon glyphicon-search'></a>");
      }
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getBalanco/", dataType: 'json', success: function(result){

        var anc = 0;
        for(var i = 0; i < result[0].length; i++){
            anc += parseInt(result[0][i]['m_Item2']);
        }

        $(".balancoItems").append("<tr>" +
              "<td class='text-center'> ATIVO</td>" +
              "<td></td></tr>");

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='anc' data-target='.anc'>" +
              "<td>Ativo não corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

    for (var i = 0; i < result[0].length; i++){
      if(result[0][i]['m_Item2'] != 0){
        $(".balancoItems").append("<tr class='collapse anc'><td>" + result[0][i]["m_Item1"] +
            "</td><td class='text-right'>" + result[0][i]["m_Item2"].toFixed(2) + "</td></tr>");
        }
    }

          $(".balancoItems").append("<tr>" +
              "<td class='text-right'>Subtotal</td>" +
              "<td class='text-right'>" +  anc.toFixed(2) + "</td></tr>");

        var ac = 0;
        for(var i = 0; i < result[1].length; i++){
            ac += parseInt(result[1][i]['m_Item2']);
        }

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='ac' data-target='.ac'>" +
              "<td>Ativo corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

    for (var i = 0; i < result[1].length; i++){
        if(result[1][i]['m_Item2'] != 0){
          $(".balancoItems").append("<tr class='collapse ac'><td>" + result[1][i]["m_Item1"] +
              "</td><td class='text-right'>" + result[1][i]["m_Item2"].toFixed(2) + "</td></tr>");
      }
    }
          $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Subtotal</td>" +
            "<td class='text-right'>" +  ac.toFixed(2) + "</td></tr>");

          var ta = anc + ac;
          $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Total do ativo</td>" +
            "<td class='text-right'>" +  ta.toFixed(2) + "</td></tr>");


        $(".balancoItems").append("<tr>" +
              "<td class='text-center'> CAPITAL PROPRIO E PASSIVO</td>" +
              "<td></td></tr>");

        var cp = 0;
        for(i = 0; i < result[2].length; i++){

          cp += parseInt(result[2][i]['m_Item2']);
        }

        this.cp = cp;

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='cp' data-target='.cp'>" +
              "<td>Capital próprio</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

      for (i = 0; i < result[2].length; i++){
        if(result[2][i]['m_Item2'] !== 0){
          $(".balancoItems").append("<tr class='collapse cp'><td>" + result[2][i]["m_Item1"] +
              "</td><td class='text-right'>" + result[2][i]["m_Item2"].toFixed(2) + "</td></tr>");
        }
    }

        $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Total do capital próprio</td>" +
            "<td class='text-right'>" +  cp.toFixed(2) + "</td></tr>");


      var pnc = 0;
        for(i = 0; i < result[3].length; i++){
            pnc += parseInt(result[3][i]['m_Item2']);
        }

        $(".balancoItems").append("<tr class='active'>" +
              "<td>Passivo</td>" +
              "<td></td></tr>");

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='pnc' data-target='.pnc'>" +
              "<td>Passivo não corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

      for (i = 0; i < result[3].length; i++){
        if(result[3][i]['m_Item2'] !== 0){
          $(".balancoItems").append("<tr class='collapse pnc'><td>" + result[3][i]["m_Item1"] +
              "</td><td class='text-right'>" + result[3][i]["m_Item2"].toFixed(2) + "</td></tr>");
      }
    }

        $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Subtotal</td>" +
            "<td class='text-right'>" +  pnc.toFixed(2) + "</td></tr>");

      var pc = 0;
        for(i = 0; i < result[4].length; i++){
            pc += parseInt(result[4][i]['m_Item2']);
        }
        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='pc' data-target='.pc'>" +
              "<td>Passivo corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

      for (i = 0; i < result[4].length; i++){
        if(result[4][i]['m_Item2'] !== 0){
          $(".balancoItems").append("<tr class='collapse pc'><td>" + result[4][i]["m_Item1"] +
              "</td><td class='text-right'>" + result[4][i]["m_Item2"].toFixed(2) + "</td></tr>");
      }
    }

    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Subtotal</td>" +
        "<td class='text-right'>" +  pc.toFixed(2) + "</td></tr>");

    var tp = pnc+pc;
    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Total do passivo</td>" +
        "<td class='text-right'>" + tp.toFixed(2) + "</td></tr>");

    var tcpp = pnc+pc+cp;
    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Total do capital próprio e do passivo</td>" +
        "<td class='text-right'>" +  tcpp.toFixed(2) + "</td></tr>");

    $(".loading1").addClass('hidden');
  }});
});
