$(document).ready(function () {
    $('a[data-toggle="collapse"]').each(function (index, item) {
        var target = $(this).attr('href');
        var key = 'settings_collapse_' + target;
        var value = localStorage.getItem(key);

        if (value === "expanded" && $(item).hasClass('collapsed')) {
            $(item).trigger('click');
        }
    });

    $('a[data-toggle="collapse"]').on('click', function () {
        var target = $(this).attr('href');
        var key = 'settings_collapse_' + target;
        var value = $(this).hasClass('collapsed') ? 'expanded' : 'collapsed';

        localStorage.setItem(key, value);
    });

    $('a.btn-addSubject').on('click', function () {
        var $categoryCollapseBtn = $(this).parent().parent().find('a[data-toggle="collapse"]');
        var target = $categoryCollapseBtn.attr('href');
        var key = 'settings_collapse_' + target;

        localStorage.setItem(key, 'expanded');
    });

    $('textarea').autogrow();
    $('textarea').each(function () {
        this.style.height = (this.scrollHeight) + "px";
    });

    $('input[onEnterTriggerClick]').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            $($(this).attr('onEnterTriggerClick')).trigger('click');
        }
    });

    $('input.exapand-width').focus(function () {
        var expandWith = parseInt($(this).attr('data-expandWith')) + $(this).width();
        $(this).attr('data-default', $(this).width());
        $(this).animate({ width: expandWith }, 'slow');
    }).blur(function () {
        var w = $(this).attr('data-default');
        $(this).animate({ width: w }, 'slow');
    });

    $('div.post-text').each(function () {
        var $postText = $(this);
        var $post = $postText.closest('div.post-wrapper');
        var $postThumbnail = $('div.post-info > div.thumbnail', $post);
        $postText.css('min-height', $postThumbnail.outerHeight());
    });
});