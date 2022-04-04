const gulp = require('gulp');

gulp.task('package:markdown-it', () => {
  return gulp
    .src('node_modules/markdown-it/dist/markdown-it.min.js')
    .pipe(gulp.dest('web/wwwroot/lib/markdown-it'));
});

gulp.task('packages', gulp.parallel('package:markdown-it'));