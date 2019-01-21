(function ($) {
   
    $.fn.printArea = function () {
       
        var ele = $(this);
        var iframeId = "printArea";
        removePrintArea(iframeId);
      
       
        var iframeStyle = 'position:absolute;width:0px;height:0px;left:-500px;top:-500px;';
        iframe = document.createElement('IFRAME');
        $(iframe).attr({
            style: iframeStyle,
            id: iframeId
        });

        document.body.appendChild(iframe);
   
        var doc = iframe.contentWindow.document;
        doc.open();
        $(document).find("link").filter(function () {
            return $(this).attr("rel").toLowerCase() == "stylesheet";
        }).each(
                function () {
                    doc.write('<link type="text/css" rel="stylesheet" href="'
                            + $(this).attr("href") + '" >');
                });
        doc.write('<div class="' + $(ele).attr("class") + '">' + $(ele).html()
                + '</div>');
        doc.close();
        $('iframe#printArea').contents().find("input[type='submit']").remove();
        $('iframe#printArea').contents().find("input[type='button']").remove();
        //var frameWindow = document.getElementById(iframeId).contentWindow;
        var frameWindow = document.getElementById("printArea").contentWindow;
       
        frameWindow.close();
        frameWindow.focus();
        setTimeout(function () { frameWindow.print(); }, 500);
      
       
    }
    var removePrintArea = function (id) {
        $("iframe#" + id).remove();
    };
})(jQuery);