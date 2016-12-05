$(document).ready(function(){
  var ref = getUrlParameter("product");

  $.ajax({url: "http://localhost:49822/api/product/Get_top10p/" + ref, dataType: 'json', success: function(result){
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
                "<td>" + result[i].valor + "</td>" +
            "</tr>"
        );
    }


    $(".melhores_c").html("Melhores Produtos");

  }});

  $.ajax({url: "http://localhost:49822/api/product/Get_Evolution/" + ref, dataType: 'json', success: function(result){
    var data = [];
    for(var k in result){
      data.push({y: result[k].date, a: result[k].value, b: "0"});
    }

    Morris.Area({
      "element" : "morris-area-chart",
      "data" : data,
      pointSize: 0,
      "xkey": 'y',
      "ykeys": ['a', 'b'],
      "labels": ['Series A', 'Series B'],
      "resize" : true
    });


  }});
});
