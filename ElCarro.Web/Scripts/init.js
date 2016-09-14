$(function () {
    $('.scrollspy').scrollSpy();
    $('select').material_select('destroy');
    $('.button-collapse').sideNav();
    $('.parallax').parallax();
    $('select').material_select();
    $('.modal-trigger').leanModal();

    $('.chips-initial').material_chip({
        data: [{
            tag: '1998',
        }, {
            tag: 'Honda',
        }, {
            tag: 'Civic',
        }, {
            tag: 'bumper delantero',
        }],
        placeholder: 'Añade más filtros',
    });

    $('input.autocomplete').autocomplete({
        data: {
            "bumper": null,
            "bumper delantero": null,
            "bumper trasero": null,
        }
    });
}); // end of document ready