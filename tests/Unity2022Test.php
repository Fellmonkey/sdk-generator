<?php

namespace Tests;

class Unity2022Test extends Base
{
    protected string $sdkName = 'unity';
    protected string $sdkPlatform = 'client';
    protected string $sdkLanguage = 'unity';
    protected string $version = '0.0.1';

    protected string $language = 'unity';
    protected string $class = 'Appwrite\SDK\Language\Unity';
    protected array $build = [
        'mkdir -p tests/sdks/unity/Tests',
        'cp tests/languages/unity/Tests.cs tests/sdks/unity/Tests/Tests.cs',
        'cp tests/languages/unity/Tests.cs.meta tests/sdks/unity/Tests/Tests.cs.meta',
    ];
    protected string $command =
        'docker run --network="mockapi" --rm -v $(pwd):/app -w /app/tests/sdks/unity unityci/editor:2022.3.0f1-base-1.0.1 unity-editor -batchmode -quit -runTests -testPlatform EditMode -testResults /app/tests/sdks/unity/test-results.xml';

    protected array $expectedOutput = [
        ...Base::FOO_RESPONSES,
        ...Base::BAR_RESPONSES,
        ...Base::GENERAL_RESPONSES,
        ...Base::UPLOAD_RESPONSES,
        ...Base::ENUM_RESPONSES,
        ...Base::EXCEPTION_RESPONSES,
        ...Base::OAUTH_RESPONSES,
        ...Base::QUERY_HELPER_RESPONSES,
        ...Base::PERMISSION_HELPER_RESPONSES,
        ...Base::ID_HELPER_RESPONSES
    ];
}
