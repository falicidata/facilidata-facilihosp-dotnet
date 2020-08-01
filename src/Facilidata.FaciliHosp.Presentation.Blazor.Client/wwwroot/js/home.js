function inicializeBulmaCarousel() {

    bulmaCarousel.attach('.hero-carousel', {
        navigation: true,
        slidesToScroll: 4,
        slidesToShow: 4,
        pagination: true,
        effect: 'fade',
        loop: true,
        autoplay: true,

    });
}