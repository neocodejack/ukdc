// ==============================================
// Direct Cremation Javascripts
// ==============================================

// fixed header 
jQuery(window).scroll(function() {
    if (jQuery(this).scrollTop() > 1){  
        jQuery('.site-header').addClass("sticky");
    }
    else{
        jQuery('.site-header').removeClass("sticky");
    }

});

//Menu
  jQuery(document).ready(function($){
        $('#menu-button').on('click', function(){
          $(this).toggleClass('active');
          $('#site-header-menu').fadeToggle('slow');
          $('#masthead').toggleClass('nav-activated');
        });
  });

 // Homepage
 jQuery(document).ready(function($) {
      // On Scroll Animation
      AOS.init({
        easing: 'ease',
        duration: 1000
      })
      // Responsive pricing table JS
      $( ".price_box ul" ).on( "click", "li", function() {
        var pos = $(this).index()+2;
        $(".price_box tr").find('td:not(:eq(0))').hide();
        $('.price_box td:nth-child('+pos+')').css('display','table-cell');
        $(".price_box tr").find('th:not(:eq(0))').hide();
        $('.price_box li').removeClass('active');
        $(this).addClass('active');
      })

      // Initialize the media query
        var mediaQuery = window.matchMedia('(min-width: 768px)');
        
        // Add a listen event
        mediaQuery.addListener(doSomething);
        
        // Function to do something with the media query
        function doSomething(mediaQuery) {    
          if (mediaQuery.matches) {
            $('.sep').attr('colspan',5);
          } else {
            $('.sep').attr('colspan',2);
          }
        }
        
        // On load
        doSomething(mediaQuery);

        $('.service_block').mouseover(function() {
          $('#service_block--group').addClass('activated');
            $('.service_block').mouseleave(function() {
                $('#service_block--group').removeClass('activated');
          })
        });
});


jQuery(document).ready(function($){
  //toggle the component with class accordion_body
  $('.toggle').click(function(){
    if ($('li .inner').is(':visible')) {
      $('li .inner').slideUp(300);
      $('.plusminus').text('+');
      $('.collapse-block').css('display','none');
    }
    if( $(this).next('li .inner').is(':visible')){
        $(this).next('li .inner').slideUp(300);
        $(this).children('.plusminus').text('+');
        $(this).children('.collapse-block').css('display','none');
        $(this).next('li .inner').removeClass('show');
    }else {
        $(this).next('li .inner').slideDown(300); 
        $(this).children('.plusminus').text('-');
        $(this).children('.collapse-block').css('display','block');
        $(this).next('li .inner').addClass('show');
    }
  });
  $('.testimonial_carousel').owlCarousel({
    loop: true,
    margin: 10,
    items: 1,
    dots: false,
    nav: true,
  })
  $('#blog_carousel').owlCarousel({
      items:4,
      loop:false,
      //center:true,
      margin:20,
      dots: false,
      nav: true,
      navText: ['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
      URLhashListener:true,
      autoplayHoverPause:true,
      startPosition: 'URLHash',
      responsive: {
        0: {
          items: 1
        },
        768: {
          items: 2
        },
        1024: {
          items: 3
        }
      }
  });
});

jQuery(document).ready(function($){
  
  $('ul.tabs li').click(function(){
    var tab_id = $(this).attr('data-tab');

    $('ul.tabs li').removeClass('current-tab');
    $('.tab-content').removeClass('current-tab');

    $(this).addClass('current-tab');
    $("#"+tab_id).addClass('current-tab');
  })

})

// Custom scrollbar
jQuery(document).ready(function($){
  $('#site-navigation').mCustomScrollbar({
    theme: "inset",    
    scrollButtons:{ enable: true }   
  });
});