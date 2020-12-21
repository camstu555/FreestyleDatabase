(function () {
    window.addEventListener('scroll', function (event) {
        var depth, i, layer, layers, len, movement, topDistance, translate3d;
        topDistance = this.pageYOffset;
        layers = document.querySelectorAll("[data-type='parallax']");
        for (i = 0, len = layers.length; i < len; i++) {
            layer = layers[i];
            depth = layer.getAttribute('data-depth');
            movement = -(topDistance * depth);
            translate3d = 'translate3d(0, ' + movement + 'px, 0)';
            layer.style['-webkit-transform'] = translate3d;
            layer.style['-moz-transform'] = translate3d;
            layer.style['-ms-transform'] = translate3d;
            layer.style['-o-transform'] = translate3d;
            layer.style.transform = translate3d;
        }
    });

    window.yourJsInterop = {
        transitionFunction: function (back) {
            let transitionIn = document.getElementsByClassName('transition-in')[0];
            let transitionOut = document.getElementsByClassName('transition-out')[0];

            let direction = back ? "out" : "in";

            if (transitionIn && transitionOut) {
                transitionOut.classList.remove('transition-out');
                transitionIn.classList.remove('transition-in');
            }
        }
    }
}).call(this);