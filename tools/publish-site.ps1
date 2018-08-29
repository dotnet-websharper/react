# creates build/html
rm -r build -errorAction ignore
$d = mkdir build
$d = mkdir build/html
cp -r WebSharper.React.Tests/Content build/html/
cp -r WebSharper.React.Tests/*.jpg build/html/
cp -r WebSharper.React.Tests/*.css build/html/
cp -r WebSharper.React.Tests/*.html build/html/
