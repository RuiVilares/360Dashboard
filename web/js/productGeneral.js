$(document).ready(function(){
    ajaxConfig();

    var ref = getUrlParameter("product");

  $.ajax({url: "http://localhost:49822/api/product/Get_top10p/" + ref, dataType: 'json', success: function(result){
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

    for(var i = 0; i < result.length && i < 10; i++){
        $(".shipments").prepend(
            "<tr>" +
                "<td>" + result[i].name + "</td>" +
                "<td><a href='product.html?product="+result[i].reference+"'>" + result[i].reference + "</a></td>" +
                "<td>" + parseFloat(result[i].valor).toLocaleString() + "</td>" +
            "</tr>"
        );
    }


    $(".melhores_c").html("Melhores Produtos");

  }});



  $.ajax({url: "http://localhost:49822/api/product/products_info/" + getUrlParameter("product"), dataType: 'json', success: function(result){
     $(".turnover").html(parseFloat(result[0].m_Item2.toFixed(2)).toLocaleString());
     $(".value").html(parseFloat(result[1].m_Item2.toFixed(2)).toLocaleString());
   }});

  setTimeout(function(){
    evolution($("#evolutionPivot").val());
  }, 50);

  $("#evolutionPivot").on("keyup paste mouseup", function(){
    $("#morris-area-chart").html("");
    evolution($("#evolutionPivot").val());
  });

});


function evolution(year){
  $.ajax({url: "http://localhost:49822/api/product/Get_Evolution/" + getUrlParameter("product"), dataType: 'json', data : {
            company: $.cookie("cmpny"),
            username : $.cookie("user"),
            password : $.cookie("pass"),
            year: year
        },
      success: function(result){

    var months = {
      1: "1º Trimestre",
      2: "2º Trimestre",
      3: "3º Trimestre",
      4: "4º Trimestre",
    };

    for(var k in result){
        result[k].value = parseFloat(result[k].value.toFixed(2));
        result[k].valuePrev = parseFloat(result[k].valuePrev.toFixed(2));
        result[k].date = months[result[k].date];
        console.log(result[k]);
    }



    var month = result.map(function(el){
      return {month: months[moment(el.date).month()+1], year: moment(el.date).year(), value: el.value};
    });

    Morris.Line({
        element : "morris-area-chart",
        data : result,
        pointSize: 10,
        xkey : "date",
        ykeys : ["value", "valuePrev"],
        labels : ["Ano Corrente ("+year+")", "Ano Transacto ("+(year-1)+")"],
        postUnits: "€",
        parseTime: false,
        resize: true
    });

  }});

  $(".in").removeClass("in");

};
