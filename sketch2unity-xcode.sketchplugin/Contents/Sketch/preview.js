var onRun = function(context) {
    const doc = context.document;
    const select = context.selection;
    
    this.createWindow();
    this.createWebView();
    [NSApp run];
}

function createWindow () {
    this.window = [[[NSWindow alloc]
                    initWithContentRect:NSMakeRect(0, 0, 1024, 768)
                    styleMask:NSTitledWindowMask | NSClosableWindowMask
                    backing:NSBackingStoreBuffered
                    defer:false
                    ] autorelease];
    
    this.window.center();
    this.window.makeKeyAndOrderFront_(this.window);
    
    return this;
}

function createWebView () {
    // create frame for loading content in
    var webviewFrame = NSMakeRect(0, 0, 1024, 768);
    
    // Request index.html
    var webviewFolder   = COScript.currentCOScript().env().scriptURL.path().stringByDeletingLastPathComponent()  + "/FDPreview/";
    var webviewHtmlFile = webviewFolder + "index.html";
    var requestUrl      = [NSURL fileURLWithPath:webviewHtmlFile];
    var urlRequest      = [NSMutableURLRequest requestWithURL:requestUrl];
    
    // Create the WebView, frame, and set content
    this.webView = WebView.new();
    this.webView.initWithFrame(webviewFrame);
    this.webView.mainFrame().loadRequest(urlRequest);
    this.window.contentView().addSubview(this.webView);
    
    return this;
};
