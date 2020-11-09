jQuery(function ($) {
    var $bodyEl = $('body'),
        $sidedrawerEl = $('#sidedrawer');

    function showSidedrawer() {
        // show overlay
        var options = {
            onclose: function () {
                $sidedrawerEl.removeClass('active').appendTo(document.body);
            }
        };

        var $overlayEl = $(mui.overlay('on', options));

        // show element
        $sidedrawerEl.appendTo($overlayEl);
        setTimeout(function () {
            $sidedrawerEl.addClass('active');
        }, 20);
    }

    function hideSidedrawer() {
        $bodyEl.toggleClass('hide-sidedrawer');
    }

    $('.js-show-sidedrawer').on('click', showSidedrawer);
    $('.js-hide-sidedrawer').on('click', hideSidedrawer);


    var url = window.location;
    var element = $('ul.sub-manu a').filter(function () {
        return this.href == url;
    }).addClass('active').parent();

    $("a.active").parent().parent().prev('strong').addClass('open');

    while (true) {
        if (element.is('li')) {
            element = element.parent().addClass('in');   
        } else {
            break;
        }
    }

    var $titleEls = $('strong', $sidedrawerEl);
   // $titleEls.next().hide();

    $titleEls.on('click', function () {
        $(this).toggleClass('open').next().slideToggle(200)
    });
});
