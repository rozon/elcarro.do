(function($){
  $(function(){

    $('.scrollspy').scrollSpy();
    $('select').material_select('destroy');
    $('.button-collapse').sideNav();
    $('.parallax').parallax();
    $('select').material_select();
    $('.modal-trigger').leanModal();
    $(".button-collapse").sideNav();

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

  }); // end of document ready
})(jQuery); // end of jQuery name space