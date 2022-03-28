(function () {
  var vars = getUrlVars(),
    message;

  function updateUrl($key) {
    delete vars[$key];
    var $hash = document.location.hash ? '#' + document.location.hash : '';

    var $params = Object.keys(vars)
      .map(function (x) {
        return x + '=' + vars[x];
      })
      .join('&');

    $params = $params !== undefined ? '?' + $params : '';

    var $newUrl =
      window.location.origin + window.location.pathname + $params + $hash;

    history.replaceState({}, document.title, $newUrl);
  }
  function addMessage($class, $message) {
    var div = document.createElement('div');
    div.classList.add(
      'notification',
      'is-light',
      $class,
      'has-text-centered',
      'my-0',
    );

    var button = document.createElement('button');

    button.classList.add('delete');
    button.addEventListener('click', function () {
      div.parentNode.removeChild(div);
    });

    div.innerHTML = '<p><b>' + $message + '</b></p>';

    div.insertBefore(button, div.firstChild);

    document
      .querySelector('body')
      .insertBefore(div, document.querySelector('body').firstChild);
  }

  if (vars['error']) {
    message = decodeURI(vars['error']).replace(/\+/g, / /);
    addMessage('is-danger', message);
    updateUrl('error');
  }

  if (vars['success']) {
    message = decodeURI(vars['success']).replace(/\+/g, / /);
    addMessage('is-success', message);
    updateUrl('success');
  }
  if (vars['warning']) {
    message = decodeURI(vars['warning']).replace(/\+/g, / /);
    addMessage('is-warning', message);
    updateUrl('warning');
  }
})();
