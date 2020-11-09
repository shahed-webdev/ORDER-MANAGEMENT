
jQuery(function ($) {
    //let linkData = [];
    //let storage = localStorage.getItem("linkData");

    //if (storage) {
    //    linkData = JSON.parse(storage);
    //    appendLinks(linkData);
    //} else {
    //    $.get("/Basic/GetSideManu", function (response) {
    //        localStorage.setItem("linkData", response);
    //        appendLinks(JSON.parse(response));
    //    });
    //};


    let url = "/Basic/GetSideManu";
    $.get(url, function (response) {
        appendLinks(JSON.parse(response));
    });

    function appendLinks(data) {
        let manuItem = $("#manuItem");
        let manu = `<li><a href="/Dashboard/Index"><strong> <span class="fas fa-tachometer-alt"></span> Dashboard</strong></a></li><li><a href="/Dashboard/UserProfile"><strong> <span class="fas fa-user-circle"></span> Profile</strong></a></li>`;

        $.each(data, (i, item) => {
            manu += `<strong><span class="${item.IconClass}"></span> ${item.Category
                } <i class="fas fa-caret-right"></i></strong><ul class="sub-manu">${manuLink(item.links)}</ul>`;
        });

        manuItem.html(manu);
    }

    function manuLink(data) {
        let link = "";
        $.each(data, (i, item) => {
            link += `<li><a class="links" href="/${item.Controller}/${item.Action}">${item.Title}</a></li>`;
        });
        return link;
    }
});
