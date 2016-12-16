$(document).ready(function(){
  $("#molhoButton").click(function(){
    $("#panel").append("<img src='../data/molho.jpg'>");
    $("#panel").last().css({"top": getRandom() + "px", "left": getRandom() + "px"});
  });
});


function getRandom(){
  return Math.floor(Math.random() * 500);
}
