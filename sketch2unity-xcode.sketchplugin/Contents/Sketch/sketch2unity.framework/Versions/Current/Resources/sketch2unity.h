
#import <Cocoa/Cocoa.h>

//! Project version number for .
FOUNDATION_EXPORT double VersionNumber;

//! Project version string for .
FOUNDATION_EXPORT const unsigned char VersionString[];

// In this header, you should import all the public headers of your framework using statements like #import </PublicHeader.h>

@interface sketch2unity : NSObject

- (double)getTextAuto:(NSString *)groupID;
- (double)getMaxAnchorX:(NSString *)groupID;
- (double)getMaxAnchorY:(NSString *)groupID;
- (double)getMinAnchorX:(NSString *)groupID;
- (double)getMinAnchorY:(NSString *)groupID;
- (double)getPivotX:(NSString *)groupID;
- (double)getPivotY:(NSString *)groupID;

@end
