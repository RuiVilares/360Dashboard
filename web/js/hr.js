$(document).ready(function() {
    ajaxConfig(); 
    
    $.ajax({url: "http://localhost:49822/api/rh/GetInfo/", dataType: 'json', success: function(result){
    	$(".num_func").html(result.numFuncionarios);
    	$(".avg_sal").html(result.vencimentoMensal.toLocaleString());
    	$(".mdn_sal").html(result.vencimentoMediano.toLocaleString());

    	Morris.Line({
	  	  element : "morris-area-chart",
          data : result.custosMensais,
          pointSize: 10,
          xLabels: "month",
          xkey : "mes",
          ykeys : ["custo"],
          labels : ["Custo Salarial"],
          postUnits: "€",
          parseTime: false,
          resize: true
    	});
    }});

});