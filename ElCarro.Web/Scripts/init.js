$(function () {
    $('.scrollspy').scrollSpy();
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
});