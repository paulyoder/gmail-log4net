NuGet Package Name: GMailLog4Net

Download Binary: http://yoder-solutions-libraries.googlecode.com/files/GmailLog4Net.dll

Example appender config
    <appender name="GmailAppender" type="YoderSolutions.Libs.GmailLog4Net.GmailAppender, GmailLog4Net">
      <to value="you@example.com" />
      <from value="email@gmail.com" />
      <username value="email@gmail.com" />
      <password value="email password"  />
      <subject value="Email Subject" />
      <bufferSize value="512" />
      <lossy value="true" />
      <evaluator type="log4net.Core.LevelEvaluator">
        <threshold value="ERROR"/>
      </evaluator>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      </layout>
    </appender>

