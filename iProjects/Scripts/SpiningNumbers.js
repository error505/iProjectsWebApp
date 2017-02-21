
        // Tile Stats
        $(".tile-stats").each(function (i, el) {
            var $this = $(el),
				$num = $this.find('.num'),

				start = attrDefault($num, 'start', 0),
				end = attrDefault($num, 'end', 0),
				prefix = attrDefault($num, 'prefix', ''),
				postfix = attrDefault($num, 'postfix', ''),
				duration = attrDefault($num, 'duration', 1000),
				delay = attrDefault($num, 'delay', 1000);

            if (start < end) {
                if (typeof scrollMonitor == 'undefined') {
                    $num.html(prefix + end + postfix);
                }
                else {
                    var tile_stats = scrollMonitor.create(el);

                    tile_stats.fullyEnterViewport(function () {

                        var o = { curr: start };

                        TweenLite.to(o, duration / 1000, {
                            curr: end, ease: Power1.easeInOut, delay: delay / 1000, onUpdate: function () {
                                $num.html(prefix + Math.round(o.curr) + postfix);
                            }
                        });

                        tile_stats.destroy()
                    });
                }
            }
        });


