$(document).ready(function(){
  ajaxConfig();

  $("#balancoColExButton").click(function(){
      if(balancoToggle){
          balancoToggle = false;
          $("#balanco").hide();
          $("#balancoColExButton").text("Expandir");
      }
      else{
        balancoToggle = true;
        $("#balanco").show();
        $("#balancoColExButton").text("Colapsar");
      }
  });

  $("#demoColExButton").click(function(){
      if(demoToggle){
          demoToggle = false;
          $("#demo").hide();
          $("#demoColExButton").text("Expandir");
      }
      else{
        demoToggle = true;
        $("#demo").show();
        $("#demoColExButton").text("Colapsar");
      }
  });

  $("#balanceteColExButton").click(function(){
      if(balanceteToggle){
          balanceteToggle = false;
          $("#balancete").hide();
          $("#balanceteColExButton").text("Expandir");
      }
      else{
        balanceteToggle = true;
        $("#balancete").show();
        $("#balanceteColExButton").text("Colapsar");
      }
  });

  $.ajax({url: "http://localhost:49822/api/accounting/getAtivos_Passivos/", dataType: 'json', success: function(result){
      $(".ativos").html(parseFloat(result.m_Item1.toFixed(2)).toLocaleString() + " €");
      $(".passivos").html(parseFloat(result.m_Item2.toFixed(2)).toLocaleString() + " €");
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getDemoResultados/", dataType: 'json', success: function(result){
    var total = 0.0;
    for (var i = 0; i < result[0].length; i++){
      $(".demoItems").append("<tr><td>" + result[0][i]["m_Item1"] +
        "</td><td class='text-right'>" + parseFloat(result[0][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
      total += result[0][i]["m_Item2"];

      if (i == 10){
        $(".demoItems").append("<tr>" +
            "<td class='text-right'>Resultado antes de depreciações, gastos de financiamento e impostos</td>" +
            "<td class='text-right'>" +  parseFloat(total.toFixed(2)).toLocaleString() + "</td></tr>");
      }

      if (i == 11){
        $(".demoItems").append("<tr>" +
            "<td class='text-right'>Resultado operacional (antes de gastos de financiamento e impostos)</td>" +
            "<td class='text-right'>" +  parseFloat(total.toFixed(2)).toLocaleString() + "</td></tr>");
      }

      if (i == 12){
        $(".demoItems").append("<tr>" +
            "<td class='text-right'>Resultado antes de impostos</td>" +
            "<td class='text-right'>" +  parseFloat(total.toFixed(2)).toLocaleString() + "</td></tr>");
      }

      if (i == 13){
        $(".demoItems").append("<tr>" +
            "<td class='text-right'>Resultado liquido do periodo</td>" +
            "<td class='text-right'>" +  parseFloat(total.toFixed(2)).toLocaleString() + "</td></tr>");
      }
    }
    $(".loading").addClass('hidden');
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getBalancete/", dataType: 'json', success: function(result){
      for(var i = 0; i < result.length; i++){
          var ob = result[i];

          if (i < 10){
            $(".balanceteItems").append('<tr><td>' + ob.codConta + '</td>' +
            "<td>" + ob.nomeConta + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.credito.toFixed(2)).toLocaleString() + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.debito.toFixed(2)).toLocaleString() + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.saldo.toFixed(2)).toLocaleString() + '</td></tr>');
          }
          else {
            $(".balanceteItems").append("<tr class='collapse balancete'><td>" + ob.codConta + '</td>' +
            "<td>" + ob.nomeConta + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.credito.toFixed(2)).toLocaleString() + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.debito.toFixed(2)).toLocaleString() + '</td>' +
            "<td class='text-right'>" + parseFloat(ob.saldo.toFixed(2)).toLocaleString() + '</td></tr>');
          }

          $(".loading2").addClass('hidden');

      }
      if (result.length >= 10){
        $("#seeMore").html("<a class='clickable pull-right btn' data-toggle='collapse' id='balancete' data-target='.balancete'><span class='glyphicon glyphicon glyphicon-search'></a>");
      }
  }});

  $.ajax({url: "http://localhost:49822/api/accounting/getBalanco/", dataType: 'json', success: function(result){

        var anc = 0.0;
        for(var i = 0; i < result[0].length; i++){
            anc += parseFloat(result[0][i]['m_Item2']);
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
            "</td><td class='text-right'>" + parseFloat(result[0][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
        }
    }

          $(".balancoItems").append("<tr>" +
              "<td class='text-right'>Subtotal</td>" +
              "<td class='text-right'>" +  parseFloat(anc.toFixed(2)).toLocaleString() + "</td></tr>");

        var ac = 0.0;
        for(var i = 0; i < result[1].length; i++){
            ac += parseFloat(result[1][i]['m_Item2']);
        }

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='ac' data-target='.ac'>" +
              "<td>Ativo corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

    for (var i = 0; i < result[1].length; i++){
        if(result[1][i]['m_Item2'] != 0){
          $(".balancoItems").append("<tr class='collapse ac'><td>" + result[1][i]["m_Item1"] +
              "</td><td class='text-right'>" + parseFloat(result[1][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
      }
    }
          $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Subtotal</td>" +
            "<td class='text-right'>" +  parseFloat(ac.toFixed(2)).toLocaleString() + "</td></tr>");

          var ta = anc + ac;
          $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Total do ativo</td>" +
            "<td class='text-right'>" +  parseFloat(ta.toFixed(2)).toLocaleString() + "</td></tr>");


        $(".balancoItems").append("<tr>" +
              "<td class='text-center'> CAPITAL PROPRIO E PASSIVO</td>" +
              "<td></td></tr>");

        var cp = 0.0;
        for(i = 0; i < result[2].length; i++){

          cp += parseFloat(result[2][i]['m_Item2']);
        }

        this.cp = cp;

        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='cp' data-target='.cp'>" +
              "<td>Capital próprio</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

      for (i = 0; i < result[2].length; i++){
        if(result[2][i]['m_Item2'] !== 0){
          $(".balancoItems").append("<tr class='collapse cp'><td>" + result[2][i]["m_Item1"] +
              "</td><td class='text-right'>" + parseFloat(result[2][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
        }
    }

        $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Total do capital próprio</td>" +
            "<td class='text-right'>" +  parseFloat(cp.toFixed(2)).toLocaleString() + "</td></tr>");


      var pnc = 0.0;
        for(i = 0; i < result[3].length; i++){
            pnc += parseFloat(result[3][i]['m_Item2']);
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
              "</td><td class='text-right'>" + parseFloat(result[3][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
      }
    }

        $(".balancoItems").append("<tr>" +
            "<td class='text-right'>Subtotal</td>" +
            "<td class='text-right'>" +  parseFloat(pnc.toFixed(2)).toLocaleString() + "</td></tr>");

      var pc = 0;
        for(i = 0; i < result[4].length; i++){
            pc += parseFloat(result[4][i]['m_Item2']);
        }
        $(".balancoItems").append("<tr class='active clickable'" +
          "data-toggle='collapse' id='pc' data-target='.pc'>" +
              "<td>Passivo corrente</td>" +
              "<td><span class='glyphicon glyphicon glyphicon-search pull-right'></span></td></tr>");

      for (i = 0; i < result[4].length; i++){
        if(result[4][i]['m_Item2'] !== 0){
          $(".balancoItems").append("<tr class='collapse pc'><td>" + result[4][i]["m_Item1"] +
              "</td><td class='text-right'>" + parseFloat(result[4][i]["m_Item2"].toFixed(2)).toLocaleString() + "</td></tr>");
      }
    }

    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Subtotal</td>" +
        "<td class='text-right'>" +  parseFloat(pc.toFixed(2)).toLocaleString() + "</td></tr>");

    var tp = pnc+pc;
    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Total do passivo</td>" +
        "<td class='text-right'>" + parseFloat(tp.toFixed(2)).toLocaleString() + "</td></tr>");

    var tcpp = pnc+pc+cp;
    $(".balancoItems").append("<tr>" +
        "<td class='text-right'>Total do capital próprio e do passivo</td>" +
        "<td class='text-right'>" +  parseFloat(tcpp.toFixed(2)).toLocaleString() + "</td></tr>");

    $(".loading1").addClass('hidden');

    $("#financialAutonomy").html(parseFloat((cp/(ac+anc))*100).toFixed(2));

  }});
  $("#balanco").hide();
  $("#balancete").hide();
  $("#demo").hide();
});

var balancoToggle = false;
var balanceteToggle = false;
var demoToggle = false;
